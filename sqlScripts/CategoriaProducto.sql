--categorias de productos

insert CATALOGO(Id_Catalogo, Identificador, CodigoParametro, Descripcion, 
Fecha_Creacion, Usr_Creacion, Publico, Id_Entidad)
select 15, '1', 'CategoriaProducto', 'Categoría Defecto', GETDATE(), 'enunez', 0, 1

select * from CATALOGO
where CodigoParametro = 'CategoriaProducto'