INSERT INTO dbo.gen_proveedor 
        (codempresa, codproveedor, nomproveedor, apellidoproveedor, duiproveedor, 
         nitproveedor, nrfproveedor, estado, telefonoproveedor, direccionproveedor)
        VALUES 
        (@codempresa, @codproveedor, @nomproveedor, @apellidoproveedor, @duiproveedor, 
         @nitproveedor, @nrfproveedor, @estado, @telefonoproveedor, @direccionproveedor)