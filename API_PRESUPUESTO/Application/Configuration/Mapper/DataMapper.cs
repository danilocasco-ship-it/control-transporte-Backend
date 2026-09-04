using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Reflection;

namespace Application.Configuration
{
    public class DataMapper: IDataMapper
    {
        public List<T> MapearDataTable<T>(System.Data.DataTable dataTable) where T : new()
        {
            var result = new List<T>();
            if (dataTable is null)
            {
                return result;
            }
            foreach (System.Data.DataRow row in dataTable.Rows)
            {
                var item = new T();

                foreach (System.Data.DataColumn column in dataTable.Columns)
                {
                    // Obtener el nombre de la propiedad
                    string propertyName = column.ColumnName;

                    // Obtener el valor de la celda
                    object cellValue = row[column];

                    // Asignar el valor a la propiedad de la clase utilizando reflection
                    SetProperty(item, propertyName, cellValue);
                }

                result.Add(item);
            }

            return result;
        }

        //private void SetProperty(object obj, string propertyName, object value)
        //{
        //    var property = obj.GetType().GetProperty(propertyName);

        //    if (property != null && value != DBNull.Value)
        //    {
        //        property.SetValue(obj, value, null);
        //    }
        //}

private void SetProperty(object obj, string propertyName, object value)
    {
        var prop = obj.GetType().GetProperty(propertyName, BindingFlags.Public | BindingFlags.Instance);
        if (prop == null) return;

        // 1) Sin valor o nulo de BD
        if (value == null || value == DBNull.Value) return;

        // 2) Tipo destino (maneja Nullable<T>)
        var targetType = Nullable.GetUnderlyingType(prop.PropertyType) ?? prop.PropertyType;

        try
        {
            object converted = value;

            // 3) Si viene como string vacío, no seteamos
            var asString = value as string;
            if (asString != null && string.IsNullOrWhiteSpace(asString)) return;

            // 4) Si ya es del tipo destino, asigna; si no, convierte
            if (!targetType.IsInstanceOfType(value))
            {
                if (targetType.IsEnum)
                {
                    converted = Enum.Parse(targetType, value.ToString());
                }
                else if (targetType == typeof(Guid))
                {
                    converted = Guid.Parse(value.ToString());
                }
                else
                {
                    // Conversión genérica: int, long, decimal, double, bool, DateTime, etc.
                    converted = Convert.ChangeType(value, targetType, CultureInfo.InvariantCulture);
                }
            }

            prop.SetValue(obj, converted, null);
        }
        catch
        {
            // Opcional: loguea si quieres diagnosticar columnas problemáticas
            // _logger?.LogWarning(ex, "No se pudo convertir {Property}='{Value}' a {Target}", propertyName, value, targetType);
            // Silenciar para no romper todo el mapeo
        }
    }


    public T MapDataTableToObject<T>(DataTable dataTable) where T : new()
        {
            //if (dataTable.Rows.Count == 0)
            //{
            //    return new T(); // O lanzar una excepción si es necesario
            //}
            if (dataTable == null)
            {
                throw new ArgumentNullException(nameof(dataTable), "El DataTable está null.");
            }

            if (dataTable.Rows.Count == 0)
            {
                return new T(); // o lanza otra excepción
            }



            DataRow row = dataTable.Rows[0];
            return MapDataRowToObject<T>(row);
        }

        public T MapDictionaryToObject<T>(Dictionary<string, object> dictionary) where T : new()
        {
            try
            {
                T result = new T();

                foreach (var kvp in dictionary)
                {
                    // Quitar el símbolo "@" de la clave del diccionario si está presente
                    string key = kvp.Key.StartsWith("@") ? kvp.Key.Substring(1) : kvp.Key;

                    foreach (PropertyInfo propertyInfo in typeof(T).GetProperties())
                    {
                        if (propertyInfo != null && kvp.Value != DBNull.Value)
                        {
                            if (propertyInfo.Name == key)
                            {
                                object value = kvp.Value;
                                propertyInfo.SetValue(result, Convert.ChangeType(value, propertyInfo.PropertyType));
                            }
                        }
                    }
                }

                return result;
            }
            catch (Exception ex)
            {
                // Opcional: manejar o registrar el error
                Console.WriteLine($"Error al mapear diccionario a objeto: {ex.Message}");
                return default(T);
            }
        }

        private T MapDataRowToObject<T>(DataRow row) where T : new()
        {
            try
            {
                T result = new T();

                foreach (PropertyInfo propertyInfo in typeof(T).GetProperties())
                {
                    if (row.Table.Columns.Contains(propertyInfo.Name) && row[propertyInfo.Name] != DBNull.Value)
                    {
                        object value = row[propertyInfo.Name];
                        propertyInfo.SetValue(result, Convert.ChangeType(value, propertyInfo.PropertyType));
                        Console.WriteLine($"Mapeando propiedad: {propertyInfo.Name} con valor: {row[propertyInfo.Name]}");



                    }
                }

             

                return result;

            }
            catch (Exception ex)
            {
                return default(T);
            }
           
        }

        
    }
}
