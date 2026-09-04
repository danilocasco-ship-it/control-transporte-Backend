




select * from Query_pre_vs_consu_Obra where numpre in 
(
	select numpre from pre_presupuesto where 
	(codempresa = @codempresa or @codempresa IS NULL or @codempresa = 0)
	and (codsucursal = @codsucursal or @codsucursal IS NULL or @codsucursal = 0)
	and (codproyecto = @codproyecto or @codproyecto IS NULL or @codproyecto = 0)
	and (poligono = @poligono or @poligono IS NULL or @poligono = '')
	and (lote = @lote or @lote IS NULL or @lote = '')
)