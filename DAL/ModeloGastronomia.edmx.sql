
-- --------------------------------------------------
-- Entity Designer DDL Script for SQL Server 2005, 2008, 2012 and Azure
-- --------------------------------------------------
-- Date Created: 04/21/2020 18:25:04
-- Generated from EDMX file: A:\Facultad\Laboratorio Computaciion  II\2016 (Regular)\Final Gastronomia (Aprobado) febrero 2018\DAL\ModeloGastronomia.edmx
-- --------------------------------------------------

SET QUOTED_IDENTIFIER OFF;
GO
USE [Gastronomia];
GO
IF SCHEMA_ID(N'dbo') IS NULL EXECUTE(N'CREATE SCHEMA [dbo]');
GO

-- --------------------------------------------------
-- Dropping existing FOREIGN KEY constraints
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[FK_UsuarioPerfil_Usuario]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[UsuarioPerfil] DROP CONSTRAINT [FK_UsuarioPerfil_Usuario];
GO
IF OBJECT_ID(N'[dbo].[FK_UsuarioPerfil_Perfil]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[UsuarioPerfil] DROP CONSTRAINT [FK_UsuarioPerfil_Perfil];
GO
IF OBJECT_ID(N'[dbo].[FK_PerfilPermiso_Perfil]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[PerfilPermiso] DROP CONSTRAINT [FK_PerfilPermiso_Perfil];
GO
IF OBJECT_ID(N'[dbo].[FK_PerfilPermiso_Permiso]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[PerfilPermiso] DROP CONSTRAINT [FK_PerfilPermiso_Permiso];
GO
IF OBJECT_ID(N'[dbo].[FK_ClienteReserva]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Comprobantes_Reserva] DROP CONSTRAINT [FK_ClienteReserva];
GO
IF OBJECT_ID(N'[dbo].[FK_EmpleadoReserva]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Comprobantes_Reserva] DROP CONSTRAINT [FK_EmpleadoReserva];
GO
IF OBJECT_ID(N'[dbo].[FK_MesaSalon]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Comprobantes_Salon] DROP CONSTRAINT [FK_MesaSalon];
GO
IF OBJECT_ID(N'[dbo].[FK_MozoSalon]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Comprobantes_Salon] DROP CONSTRAINT [FK_MozoSalon];
GO
IF OBJECT_ID(N'[dbo].[FK_ProductoDetalleComprobante]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[DetalleComprobantes] DROP CONSTRAINT [FK_ProductoDetalleComprobante];
GO
IF OBJECT_ID(N'[dbo].[FK_RubroSubRubro]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[SubRubros] DROP CONSTRAINT [FK_RubroSubRubro];
GO
IF OBJECT_ID(N'[dbo].[FK_SubRubroProducto]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Productos] DROP CONSTRAINT [FK_SubRubroProducto];
GO
IF OBJECT_ID(N'[dbo].[FK_MarcaProducto]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Productos] DROP CONSTRAINT [FK_MarcaProducto];
GO
IF OBJECT_ID(N'[dbo].[FK_SalonDetalleSalon]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[DetalleComprobantes_DetalleSalon] DROP CONSTRAINT [FK_SalonDetalleSalon];
GO
IF OBJECT_ID(N'[dbo].[FK_ClienteDelivery]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Comprobantes_Delivery] DROP CONSTRAINT [FK_ClienteDelivery];
GO
IF OBJECT_ID(N'[dbo].[FK_DeliveryDetalleDelivery]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[DetalleComprobantes_DetalleDelivery] DROP CONSTRAINT [FK_DeliveryDetalleDelivery];
GO
IF OBJECT_ID(N'[dbo].[FK_CadeteDelivery]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Comprobantes_Delivery] DROP CONSTRAINT [FK_CadeteDelivery];
GO
IF OBJECT_ID(N'[dbo].[FK_ListaPrecioAlicuota]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Alicuotas] DROP CONSTRAINT [FK_ListaPrecioAlicuota];
GO
IF OBJECT_ID(N'[dbo].[FK_ListaPrecioListaPrecioProducto]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[ListaPrecioProductos] DROP CONSTRAINT [FK_ListaPrecioListaPrecioProducto];
GO
IF OBJECT_ID(N'[dbo].[FK_ProductoListaPrecioProducto]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[ListaPrecioProductos] DROP CONSTRAINT [FK_ProductoListaPrecioProducto];
GO
IF OBJECT_ID(N'[dbo].[FK_ProveedorProducto_Proveedor]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[ProveedorProducto] DROP CONSTRAINT [FK_ProveedorProducto_Proveedor];
GO
IF OBJECT_ID(N'[dbo].[FK_ProveedorProducto_Producto]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[ProveedorProducto] DROP CONSTRAINT [FK_ProveedorProducto_Producto];
GO
IF OBJECT_ID(N'[dbo].[FK_EmpleadoUsuario]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Personas_Empleado] DROP CONSTRAINT [FK_EmpleadoUsuario];
GO
IF OBJECT_ID(N'[dbo].[FK_RubroProveedor]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Personas_Proveedor] DROP CONSTRAINT [FK_RubroProveedor];
GO
IF OBJECT_ID(N'[dbo].[FK_CondicionIvaPersonaJuridica]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Personas_PersonaJuridica] DROP CONSTRAINT [FK_CondicionIvaPersonaJuridica];
GO
IF OBJECT_ID(N'[dbo].[FK_ClienteSalon]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Comprobantes_Salon] DROP CONSTRAINT [FK_ClienteSalon];
GO
IF OBJECT_ID(N'[dbo].[FK_MesaReserva]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Comprobantes_Reserva] DROP CONSTRAINT [FK_MesaReserva];
GO
IF OBJECT_ID(N'[dbo].[FK_ClienteCuentaCorriente]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[CuentasCorriente] DROP CONSTRAINT [FK_ClienteCuentaCorriente];
GO
IF OBJECT_ID(N'[dbo].[FK_EmpleadoFactura]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Facturas] DROP CONSTRAINT [FK_EmpleadoFactura];
GO
IF OBJECT_ID(N'[dbo].[FK_ClienteFactura]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Facturas] DROP CONSTRAINT [FK_ClienteFactura];
GO
IF OBJECT_ID(N'[dbo].[FK_CuentaCorrienteFactura]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Facturas] DROP CONSTRAINT [FK_CuentaCorrienteFactura];
GO
IF OBJECT_ID(N'[dbo].[FK_ComprobanteFactura]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Facturas] DROP CONSTRAINT [FK_ComprobanteFactura];
GO
IF OBJECT_ID(N'[dbo].[FK_FacturaMovimiento]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Movimientos] DROP CONSTRAINT [FK_FacturaMovimiento];
GO
IF OBJECT_ID(N'[dbo].[FK_ClienteMovimiento]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Movimientos] DROP CONSTRAINT [FK_ClienteMovimiento];
GO
IF OBJECT_ID(N'[dbo].[FK_ProductoPedidoProducto]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[PedidosProducto] DROP CONSTRAINT [FK_ProductoPedidoProducto];
GO
IF OBJECT_ID(N'[dbo].[FK_ProveedorPedidoProducto]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[PedidosProducto] DROP CONSTRAINT [FK_ProveedorPedidoProducto];
GO
IF OBJECT_ID(N'[dbo].[FK_ProveedorMovimiento]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Movimientos] DROP CONSTRAINT [FK_ProveedorMovimiento];
GO
IF OBJECT_ID(N'[dbo].[FK_PersonaFisica_inherits_Persona]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Personas_PersonaFisica] DROP CONSTRAINT [FK_PersonaFisica_inherits_Persona];
GO
IF OBJECT_ID(N'[dbo].[FK_Cliente_inherits_PersonaFisica]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Personas_Cliente] DROP CONSTRAINT [FK_Cliente_inherits_PersonaFisica];
GO
IF OBJECT_ID(N'[dbo].[FK_Reserva_inherits_Comprobante]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Comprobantes_Reserva] DROP CONSTRAINT [FK_Reserva_inherits_Comprobante];
GO
IF OBJECT_ID(N'[dbo].[FK_Empleado_inherits_PersonaFisica]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Personas_Empleado] DROP CONSTRAINT [FK_Empleado_inherits_PersonaFisica];
GO
IF OBJECT_ID(N'[dbo].[FK_Salon_inherits_Comprobante]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Comprobantes_Salon] DROP CONSTRAINT [FK_Salon_inherits_Comprobante];
GO
IF OBJECT_ID(N'[dbo].[FK_DetalleSalon_inherits_DetalleComprobante]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[DetalleComprobantes_DetalleSalon] DROP CONSTRAINT [FK_DetalleSalon_inherits_DetalleComprobante];
GO
IF OBJECT_ID(N'[dbo].[FK_Delivery_inherits_Comprobante]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Comprobantes_Delivery] DROP CONSTRAINT [FK_Delivery_inherits_Comprobante];
GO
IF OBJECT_ID(N'[dbo].[FK_DetalleDelivery_inherits_DetalleComprobante]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[DetalleComprobantes_DetalleDelivery] DROP CONSTRAINT [FK_DetalleDelivery_inherits_DetalleComprobante];
GO
IF OBJECT_ID(N'[dbo].[FK_PersonaJuridica_inherits_Persona]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Personas_PersonaJuridica] DROP CONSTRAINT [FK_PersonaJuridica_inherits_Persona];
GO
IF OBJECT_ID(N'[dbo].[FK_Proveedor_inherits_PersonaJuridica]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Personas_Proveedor] DROP CONSTRAINT [FK_Proveedor_inherits_PersonaJuridica];
GO

-- --------------------------------------------------
-- Dropping existing tables
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[Personas]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Personas];
GO
IF OBJECT_ID(N'[dbo].[Usuarios]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Usuarios];
GO
IF OBJECT_ID(N'[dbo].[Perfiles]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Perfiles];
GO
IF OBJECT_ID(N'[dbo].[Permisos]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Permisos];
GO
IF OBJECT_ID(N'[dbo].[Comprobantes]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Comprobantes];
GO
IF OBJECT_ID(N'[dbo].[Mesas]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Mesas];
GO
IF OBJECT_ID(N'[dbo].[DetalleComprobantes]', 'U') IS NOT NULL
    DROP TABLE [dbo].[DetalleComprobantes];
GO
IF OBJECT_ID(N'[dbo].[Productos]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Productos];
GO
IF OBJECT_ID(N'[dbo].[Marcas]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Marcas];
GO
IF OBJECT_ID(N'[dbo].[Rubros]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Rubros];
GO
IF OBJECT_ID(N'[dbo].[SubRubros]', 'U') IS NOT NULL
    DROP TABLE [dbo].[SubRubros];
GO
IF OBJECT_ID(N'[dbo].[ListaPrecios]', 'U') IS NOT NULL
    DROP TABLE [dbo].[ListaPrecios];
GO
IF OBJECT_ID(N'[dbo].[Alicuotas]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Alicuotas];
GO
IF OBJECT_ID(N'[dbo].[ListaPrecioProductos]', 'U') IS NOT NULL
    DROP TABLE [dbo].[ListaPrecioProductos];
GO
IF OBJECT_ID(N'[dbo].[CondicionIvas]', 'U') IS NOT NULL
    DROP TABLE [dbo].[CondicionIvas];
GO
IF OBJECT_ID(N'[dbo].[Facturas]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Facturas];
GO
IF OBJECT_ID(N'[dbo].[CuentasCorriente]', 'U') IS NOT NULL
    DROP TABLE [dbo].[CuentasCorriente];
GO
IF OBJECT_ID(N'[dbo].[Movimientos]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Movimientos];
GO
IF OBJECT_ID(N'[dbo].[PedidosProducto]', 'U') IS NOT NULL
    DROP TABLE [dbo].[PedidosProducto];
GO
IF OBJECT_ID(N'[dbo].[Personas_PersonaFisica]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Personas_PersonaFisica];
GO
IF OBJECT_ID(N'[dbo].[Personas_Cliente]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Personas_Cliente];
GO
IF OBJECT_ID(N'[dbo].[Comprobantes_Reserva]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Comprobantes_Reserva];
GO
IF OBJECT_ID(N'[dbo].[Personas_Empleado]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Personas_Empleado];
GO
IF OBJECT_ID(N'[dbo].[Comprobantes_Salon]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Comprobantes_Salon];
GO
IF OBJECT_ID(N'[dbo].[DetalleComprobantes_DetalleSalon]', 'U') IS NOT NULL
    DROP TABLE [dbo].[DetalleComprobantes_DetalleSalon];
GO
IF OBJECT_ID(N'[dbo].[Comprobantes_Delivery]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Comprobantes_Delivery];
GO
IF OBJECT_ID(N'[dbo].[DetalleComprobantes_DetalleDelivery]', 'U') IS NOT NULL
    DROP TABLE [dbo].[DetalleComprobantes_DetalleDelivery];
GO
IF OBJECT_ID(N'[dbo].[Personas_PersonaJuridica]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Personas_PersonaJuridica];
GO
IF OBJECT_ID(N'[dbo].[Personas_Proveedor]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Personas_Proveedor];
GO
IF OBJECT_ID(N'[dbo].[UsuarioPerfil]', 'U') IS NOT NULL
    DROP TABLE [dbo].[UsuarioPerfil];
GO
IF OBJECT_ID(N'[dbo].[PerfilPermiso]', 'U') IS NOT NULL
    DROP TABLE [dbo].[PerfilPermiso];
GO
IF OBJECT_ID(N'[dbo].[ProveedorProducto]', 'U') IS NOT NULL
    DROP TABLE [dbo].[ProveedorProducto];
GO

-- --------------------------------------------------
-- Creating all tables
-- --------------------------------------------------

-- Creating table 'Personas'
CREATE TABLE [dbo].[Personas] (
    [Id] bigint IDENTITY(1,1) NOT NULL,
    [Direccion] nvarchar(max)  NOT NULL,
    [Teléfono] nvarchar(max)  NOT NULL,
    [EstaEliminado] bit  NOT NULL
);
GO

-- Creating table 'Usuarios'
CREATE TABLE [dbo].[Usuarios] (
    [Id] bigint IDENTITY(1,1) NOT NULL,
    [Nombre] nvarchar(250)  NOT NULL,
    [Password] nvarchar(250)  NOT NULL,
    [EstaBloqueado] bit  NOT NULL
);
GO

-- Creating table 'Perfiles'
CREATE TABLE [dbo].[Perfiles] (
    [Id] bigint IDENTITY(1,1) NOT NULL,
    [Descripcion] nvarchar(250)  NOT NULL
);
GO

-- Creating table 'Permisos'
CREATE TABLE [dbo].[Permisos] (
    [Id] bigint IDENTITY(1,1) NOT NULL,
    [Codigo] int  NOT NULL,
    [Formulario] nvarchar(250)  NOT NULL,
    [EstaEliminado] bit  NOT NULL
);
GO

-- Creating table 'Comprobantes'
CREATE TABLE [dbo].[Comprobantes] (
    [Id] bigint IDENTITY(1,1) NOT NULL,
    [Fecha] datetime  NOT NULL
);
GO

-- Creating table 'Mesas'
CREATE TABLE [dbo].[Mesas] (
    [Id] bigint IDENTITY(1,1) NOT NULL,
    [Numero] int  NOT NULL,
    [Descripcion] nvarchar(250)  NOT NULL,
    [EstadoMesa] int  NOT NULL
);
GO

-- Creating table 'DetalleComprobantes'
CREATE TABLE [dbo].[DetalleComprobantes] (
    [Id] bigint IDENTITY(1,1) NOT NULL,
    [Cantidad] int  NOT NULL,
    [SubTotal] decimal(18,2)  NOT NULL,
    [Codigo] nvarchar(20)  NOT NULL,
    [CodigoBarra] nvarchar(200)  NOT NULL,
    [Descripcion] nvarchar(250)  NOT NULL,
    [Precio] decimal(18,2)  NOT NULL,
    [ProductoId] int  NOT NULL
);
GO

-- Creating table 'Productos'
CREATE TABLE [dbo].[Productos] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Codigo] int  NOT NULL,
    [CodigoBarra] nvarchar(50)  NOT NULL,
    [Descripcion] nvarchar(250)  NOT NULL,
    [Stock] int  NOT NULL,
    [SubRubroId] bigint  NOT NULL,
    [MarcaId] bigint  NOT NULL,
    [EstaEliminado] bit  NOT NULL
);
GO

-- Creating table 'Marcas'
CREATE TABLE [dbo].[Marcas] (
    [Id] bigint IDENTITY(1,1) NOT NULL,
    [Codigo] int  NOT NULL,
    [Descripcion] nvarchar(150)  NOT NULL,
    [EstaEliminado] bit  NOT NULL
);
GO

-- Creating table 'Rubros'
CREATE TABLE [dbo].[Rubros] (
    [Id] bigint IDENTITY(1,1) NOT NULL,
    [Codigo] int  NOT NULL,
    [Descripcion] nvarchar(250)  NOT NULL,
    [EstaEliminado] bit  NOT NULL
);
GO

-- Creating table 'SubRubros'
CREATE TABLE [dbo].[SubRubros] (
    [Id] bigint IDENTITY(1,1) NOT NULL,
    [Codigo] int  NOT NULL,
    [Descripcion] nvarchar(250)  NOT NULL,
    [RubroId] bigint  NOT NULL,
    [EstaEliminado] bit  NOT NULL
);
GO

-- Creating table 'ListaPrecios'
CREATE TABLE [dbo].[ListaPrecios] (
    [Id] bigint IDENTITY(1,1) NOT NULL,
    [Codigo] int  NOT NULL,
    [Descripcion] nvarchar(250)  NOT NULL,
    [EstaEliminado] bit  NOT NULL
);
GO

-- Creating table 'Alicuotas'
CREATE TABLE [dbo].[Alicuotas] (
    [Id] bigint IDENTITY(1,1) NOT NULL,
    [FechaActualizacion] datetime  NOT NULL,
    [Porcentaje] decimal(18,0)  NOT NULL,
    [ListaPrecioId] bigint  NOT NULL
);
GO

-- Creating table 'ListaPrecioProductos'
CREATE TABLE [dbo].[ListaPrecioProductos] (
    [Id] bigint IDENTITY(1,1) NOT NULL,
    [ListaPrecioId] bigint  NOT NULL,
    [ProductoId] int  NOT NULL,
    [PrecioCosto] decimal(18,2)  NOT NULL,
    [PrecioPublico] decimal(18,2)  NOT NULL,
    [Alicuota] decimal(18,2)  NOT NULL,
    [FechaActualizacion] datetime  NOT NULL,
    [EstaEliminado] bit  NOT NULL
);
GO

-- Creating table 'CondicionIvas'
CREATE TABLE [dbo].[CondicionIvas] (
    [Id] bigint IDENTITY(1,1) NOT NULL,
    [Descripcion] nvarchar(max)  NOT NULL,
    [Codigo] int  NOT NULL
);
GO

-- Creating table 'Facturas'
CREATE TABLE [dbo].[Facturas] (
    [Id] bigint IDENTITY(1,1) NOT NULL,
    [Fecha] datetime  NOT NULL,
    [Estado] int  NOT NULL,
    [Total] decimal(18,0)  NOT NULL,
    [Numero] int  NOT NULL,
    [EmpleadoId] bigint  NOT NULL,
    [ClienteId] bigint  NOT NULL,
    [CuentaCorrienteId] bigint  NULL,
    [TotalAbonado] decimal(18,0)  NOT NULL,
    [Comprobante_Id] bigint  NOT NULL
);
GO

-- Creating table 'CuentasCorriente'
CREATE TABLE [dbo].[CuentasCorriente] (
    [Id] bigint IDENTITY(1,1) NOT NULL,
    [Numero] int  NOT NULL,
    [Fecha] datetime  NOT NULL,
    [EstaHabilitada] bit  NOT NULL,
    [Cliente_Id] bigint  NOT NULL
);
GO

-- Creating table 'Movimientos'
CREATE TABLE [dbo].[Movimientos] (
    [Id] bigint IDENTITY(1,1) NOT NULL,
    [Fecha] datetime  NOT NULL,
    [Tipo] int  NOT NULL,
    [Monto] decimal(18,0)  NOT NULL,
    [Numero] int  NOT NULL,
    [FacturaId] bigint  NULL,
    [ClienteId] bigint  NULL,
    [ProveedorId] bigint  NULL
);
GO

-- Creating table 'PedidosProducto'
CREATE TABLE [dbo].[PedidosProducto] (
    [Id] bigint IDENTITY(1,1) NOT NULL,
    [Numero] int  NOT NULL,
    [Fecha] datetime  NOT NULL,
    [PrecioCosto] decimal(18,0)  NOT NULL,
    [Cantidad] int  NOT NULL,
    [Total] decimal(18,0)  NOT NULL,
    [ProductoId] int  NOT NULL,
    [ProveedorId] bigint  NOT NULL
);
GO

-- Creating table 'Personas_PersonaFisica'
CREATE TABLE [dbo].[Personas_PersonaFisica] (
    [Apellido] nvarchar(250)  NOT NULL,
    [Nombre] nvarchar(250)  NOT NULL,
    [Dni] nvarchar(8)  NOT NULL,
    [Cuil] nvarchar(11)  NOT NULL,
    [Celular] nvarchar(20)  NOT NULL,
    [Id] bigint  NOT NULL
);
GO

-- Creating table 'Personas_Cliente'
CREATE TABLE [dbo].[Personas_Cliente] (
    [Codigo] int  NOT NULL,
    [estaEliminado] bit  NOT NULL,
    [ActivoParaCompra] bit  NOT NULL,
    [TieneCtaCte] bit  NOT NULL,
    [MontoMaximo] decimal(18,0)  NULL,
    [DeudaTotal] decimal(18,0)  NULL,
    [EstaOcupado] bit  NOT NULL,
    [Id] bigint  NOT NULL
);
GO

-- Creating table 'Comprobantes_Reserva'
CREATE TABLE [dbo].[Comprobantes_Reserva] (
    [ClienteId] bigint  NOT NULL,
    [FechaReserva] datetime  NOT NULL,
    [CantidadComensales] int  NOT NULL,
    [EstadoReserva] int  NOT NULL,
    [EmpleadoId] bigint  NOT NULL,
    [Observacion] nvarchar(500)  NOT NULL,
    [EstaEliminado] bit  NOT NULL,
    [Codigo] int  NOT NULL,
    [MesaId] bigint  NOT NULL,
    [Id] bigint  NOT NULL
);
GO

-- Creating table 'Personas_Empleado'
CREATE TABLE [dbo].[Personas_Empleado] (
    [Legajo] int  NOT NULL,
    [TipoEmpleado] int  NOT NULL,
    [estaEliminado] bit  NOT NULL,
    [Id] bigint  NOT NULL,
    [Usuario_Id] bigint  NOT NULL
);
GO

-- Creating table 'Comprobantes_Salon'
CREATE TABLE [dbo].[Comprobantes_Salon] (
    [MesaId] bigint  NOT NULL,
    [Cubiertos] int  NOT NULL,
    [MozoId] bigint  NOT NULL,
    [Subtotal] decimal(18,2)  NOT NULL,
    [Descuento] decimal(18,2)  NOT NULL,
    [Total] decimal(18,2)  NOT NULL,
    [EstadoSalon] int  NOT NULL,
    [ClienteId] bigint  NOT NULL,
    [Id] bigint  NOT NULL
);
GO

-- Creating table 'DetalleComprobantes_DetalleSalon'
CREATE TABLE [dbo].[DetalleComprobantes_DetalleSalon] (
    [SalonId] bigint  NOT NULL,
    [Id] bigint  NOT NULL
);
GO

-- Creating table 'Comprobantes_Delivery'
CREATE TABLE [dbo].[Comprobantes_Delivery] (
    [ClienteId] bigint  NOT NULL,
    [Total] decimal(18,2)  NOT NULL,
    [DireccionEnvio] nvarchar(400)  NOT NULL,
    [Observacion] nvarchar(400)  NULL,
    [EstadoDelivery] int  NOT NULL,
    [CadeteId] bigint  NOT NULL,
    [Descuento] decimal(18,0)  NOT NULL,
    [SubTotal] decimal(18,0)  NOT NULL,
    [Id] bigint  NOT NULL
);
GO

-- Creating table 'DetalleComprobantes_DetalleDelivery'
CREATE TABLE [dbo].[DetalleComprobantes_DetalleDelivery] (
    [DeliveryId] bigint  NOT NULL,
    [Id] bigint  NOT NULL
);
GO

-- Creating table 'Personas_PersonaJuridica'
CREATE TABLE [dbo].[Personas_PersonaJuridica] (
    [Cuit] nvarchar(20)  NOT NULL,
    [RazonSocial] nvarchar(400)  NOT NULL,
    [NombreFantacia] nvarchar(400)  NOT NULL,
    [FechaInicioActividad] datetime  NOT NULL,
    [IngresosBrutos] decimal(18,0)  NOT NULL,
    [CondicionIvaId] bigint  NOT NULL,
    [Id] bigint  NOT NULL
);
GO

-- Creating table 'Personas_Proveedor'
CREATE TABLE [dbo].[Personas_Proveedor] (
    [ApyNomContacto] nvarchar(250)  NOT NULL,
    [RubroId] bigint  NOT NULL,
    [Id] bigint  NOT NULL
);
GO

-- Creating table 'UsuarioPerfil'
CREATE TABLE [dbo].[UsuarioPerfil] (
    [Usuario_Id] bigint  NOT NULL,
    [Perfil_Id] bigint  NOT NULL
);
GO

-- Creating table 'PerfilPermiso'
CREATE TABLE [dbo].[PerfilPermiso] (
    [Perfil_Id] bigint  NOT NULL,
    [Permiso_Id] bigint  NOT NULL
);
GO

-- Creating table 'ProveedorProducto'
CREATE TABLE [dbo].[ProveedorProducto] (
    [Proveedores_Id] bigint  NOT NULL,
    [Productos_Id] int  NOT NULL
);
GO

-- --------------------------------------------------
-- Creating all PRIMARY KEY constraints
-- --------------------------------------------------

-- Creating primary key on [Id] in table 'Personas'
ALTER TABLE [dbo].[Personas]
ADD CONSTRAINT [PK_Personas]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'Usuarios'
ALTER TABLE [dbo].[Usuarios]
ADD CONSTRAINT [PK_Usuarios]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'Perfiles'
ALTER TABLE [dbo].[Perfiles]
ADD CONSTRAINT [PK_Perfiles]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'Permisos'
ALTER TABLE [dbo].[Permisos]
ADD CONSTRAINT [PK_Permisos]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'Comprobantes'
ALTER TABLE [dbo].[Comprobantes]
ADD CONSTRAINT [PK_Comprobantes]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'Mesas'
ALTER TABLE [dbo].[Mesas]
ADD CONSTRAINT [PK_Mesas]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'DetalleComprobantes'
ALTER TABLE [dbo].[DetalleComprobantes]
ADD CONSTRAINT [PK_DetalleComprobantes]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'Productos'
ALTER TABLE [dbo].[Productos]
ADD CONSTRAINT [PK_Productos]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'Marcas'
ALTER TABLE [dbo].[Marcas]
ADD CONSTRAINT [PK_Marcas]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'Rubros'
ALTER TABLE [dbo].[Rubros]
ADD CONSTRAINT [PK_Rubros]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'SubRubros'
ALTER TABLE [dbo].[SubRubros]
ADD CONSTRAINT [PK_SubRubros]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'ListaPrecios'
ALTER TABLE [dbo].[ListaPrecios]
ADD CONSTRAINT [PK_ListaPrecios]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'Alicuotas'
ALTER TABLE [dbo].[Alicuotas]
ADD CONSTRAINT [PK_Alicuotas]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'ListaPrecioProductos'
ALTER TABLE [dbo].[ListaPrecioProductos]
ADD CONSTRAINT [PK_ListaPrecioProductos]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'CondicionIvas'
ALTER TABLE [dbo].[CondicionIvas]
ADD CONSTRAINT [PK_CondicionIvas]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'Facturas'
ALTER TABLE [dbo].[Facturas]
ADD CONSTRAINT [PK_Facturas]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'CuentasCorriente'
ALTER TABLE [dbo].[CuentasCorriente]
ADD CONSTRAINT [PK_CuentasCorriente]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'Movimientos'
ALTER TABLE [dbo].[Movimientos]
ADD CONSTRAINT [PK_Movimientos]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'PedidosProducto'
ALTER TABLE [dbo].[PedidosProducto]
ADD CONSTRAINT [PK_PedidosProducto]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'Personas_PersonaFisica'
ALTER TABLE [dbo].[Personas_PersonaFisica]
ADD CONSTRAINT [PK_Personas_PersonaFisica]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'Personas_Cliente'
ALTER TABLE [dbo].[Personas_Cliente]
ADD CONSTRAINT [PK_Personas_Cliente]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'Comprobantes_Reserva'
ALTER TABLE [dbo].[Comprobantes_Reserva]
ADD CONSTRAINT [PK_Comprobantes_Reserva]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'Personas_Empleado'
ALTER TABLE [dbo].[Personas_Empleado]
ADD CONSTRAINT [PK_Personas_Empleado]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'Comprobantes_Salon'
ALTER TABLE [dbo].[Comprobantes_Salon]
ADD CONSTRAINT [PK_Comprobantes_Salon]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'DetalleComprobantes_DetalleSalon'
ALTER TABLE [dbo].[DetalleComprobantes_DetalleSalon]
ADD CONSTRAINT [PK_DetalleComprobantes_DetalleSalon]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'Comprobantes_Delivery'
ALTER TABLE [dbo].[Comprobantes_Delivery]
ADD CONSTRAINT [PK_Comprobantes_Delivery]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'DetalleComprobantes_DetalleDelivery'
ALTER TABLE [dbo].[DetalleComprobantes_DetalleDelivery]
ADD CONSTRAINT [PK_DetalleComprobantes_DetalleDelivery]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'Personas_PersonaJuridica'
ALTER TABLE [dbo].[Personas_PersonaJuridica]
ADD CONSTRAINT [PK_Personas_PersonaJuridica]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'Personas_Proveedor'
ALTER TABLE [dbo].[Personas_Proveedor]
ADD CONSTRAINT [PK_Personas_Proveedor]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Usuario_Id], [Perfil_Id] in table 'UsuarioPerfil'
ALTER TABLE [dbo].[UsuarioPerfil]
ADD CONSTRAINT [PK_UsuarioPerfil]
    PRIMARY KEY CLUSTERED ([Usuario_Id], [Perfil_Id] ASC);
GO

-- Creating primary key on [Perfil_Id], [Permiso_Id] in table 'PerfilPermiso'
ALTER TABLE [dbo].[PerfilPermiso]
ADD CONSTRAINT [PK_PerfilPermiso]
    PRIMARY KEY CLUSTERED ([Perfil_Id], [Permiso_Id] ASC);
GO

-- Creating primary key on [Proveedores_Id], [Productos_Id] in table 'ProveedorProducto'
ALTER TABLE [dbo].[ProveedorProducto]
ADD CONSTRAINT [PK_ProveedorProducto]
    PRIMARY KEY CLUSTERED ([Proveedores_Id], [Productos_Id] ASC);
GO

-- --------------------------------------------------
-- Creating all FOREIGN KEY constraints
-- --------------------------------------------------

-- Creating foreign key on [Usuario_Id] in table 'UsuarioPerfil'
ALTER TABLE [dbo].[UsuarioPerfil]
ADD CONSTRAINT [FK_UsuarioPerfil_Usuario]
    FOREIGN KEY ([Usuario_Id])
    REFERENCES [dbo].[Usuarios]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating foreign key on [Perfil_Id] in table 'UsuarioPerfil'
ALTER TABLE [dbo].[UsuarioPerfil]
ADD CONSTRAINT [FK_UsuarioPerfil_Perfil]
    FOREIGN KEY ([Perfil_Id])
    REFERENCES [dbo].[Perfiles]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_UsuarioPerfil_Perfil'
CREATE INDEX [IX_FK_UsuarioPerfil_Perfil]
ON [dbo].[UsuarioPerfil]
    ([Perfil_Id]);
GO

-- Creating foreign key on [Perfil_Id] in table 'PerfilPermiso'
ALTER TABLE [dbo].[PerfilPermiso]
ADD CONSTRAINT [FK_PerfilPermiso_Perfil]
    FOREIGN KEY ([Perfil_Id])
    REFERENCES [dbo].[Perfiles]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating foreign key on [Permiso_Id] in table 'PerfilPermiso'
ALTER TABLE [dbo].[PerfilPermiso]
ADD CONSTRAINT [FK_PerfilPermiso_Permiso]
    FOREIGN KEY ([Permiso_Id])
    REFERENCES [dbo].[Permisos]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_PerfilPermiso_Permiso'
CREATE INDEX [IX_FK_PerfilPermiso_Permiso]
ON [dbo].[PerfilPermiso]
    ([Permiso_Id]);
GO

-- Creating foreign key on [ClienteId] in table 'Comprobantes_Reserva'
ALTER TABLE [dbo].[Comprobantes_Reserva]
ADD CONSTRAINT [FK_ClienteReserva]
    FOREIGN KEY ([ClienteId])
    REFERENCES [dbo].[Personas_Cliente]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_ClienteReserva'
CREATE INDEX [IX_FK_ClienteReserva]
ON [dbo].[Comprobantes_Reserva]
    ([ClienteId]);
GO

-- Creating foreign key on [EmpleadoId] in table 'Comprobantes_Reserva'
ALTER TABLE [dbo].[Comprobantes_Reserva]
ADD CONSTRAINT [FK_EmpleadoReserva]
    FOREIGN KEY ([EmpleadoId])
    REFERENCES [dbo].[Personas_Empleado]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_EmpleadoReserva'
CREATE INDEX [IX_FK_EmpleadoReserva]
ON [dbo].[Comprobantes_Reserva]
    ([EmpleadoId]);
GO

-- Creating foreign key on [MesaId] in table 'Comprobantes_Salon'
ALTER TABLE [dbo].[Comprobantes_Salon]
ADD CONSTRAINT [FK_MesaSalon]
    FOREIGN KEY ([MesaId])
    REFERENCES [dbo].[Mesas]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_MesaSalon'
CREATE INDEX [IX_FK_MesaSalon]
ON [dbo].[Comprobantes_Salon]
    ([MesaId]);
GO

-- Creating foreign key on [MozoId] in table 'Comprobantes_Salon'
ALTER TABLE [dbo].[Comprobantes_Salon]
ADD CONSTRAINT [FK_MozoSalon]
    FOREIGN KEY ([MozoId])
    REFERENCES [dbo].[Personas_Empleado]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_MozoSalon'
CREATE INDEX [IX_FK_MozoSalon]
ON [dbo].[Comprobantes_Salon]
    ([MozoId]);
GO

-- Creating foreign key on [ProductoId] in table 'DetalleComprobantes'
ALTER TABLE [dbo].[DetalleComprobantes]
ADD CONSTRAINT [FK_ProductoDetalleComprobante]
    FOREIGN KEY ([ProductoId])
    REFERENCES [dbo].[Productos]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_ProductoDetalleComprobante'
CREATE INDEX [IX_FK_ProductoDetalleComprobante]
ON [dbo].[DetalleComprobantes]
    ([ProductoId]);
GO

-- Creating foreign key on [RubroId] in table 'SubRubros'
ALTER TABLE [dbo].[SubRubros]
ADD CONSTRAINT [FK_RubroSubRubro]
    FOREIGN KEY ([RubroId])
    REFERENCES [dbo].[Rubros]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_RubroSubRubro'
CREATE INDEX [IX_FK_RubroSubRubro]
ON [dbo].[SubRubros]
    ([RubroId]);
GO

-- Creating foreign key on [SubRubroId] in table 'Productos'
ALTER TABLE [dbo].[Productos]
ADD CONSTRAINT [FK_SubRubroProducto]
    FOREIGN KEY ([SubRubroId])
    REFERENCES [dbo].[SubRubros]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_SubRubroProducto'
CREATE INDEX [IX_FK_SubRubroProducto]
ON [dbo].[Productos]
    ([SubRubroId]);
GO

-- Creating foreign key on [MarcaId] in table 'Productos'
ALTER TABLE [dbo].[Productos]
ADD CONSTRAINT [FK_MarcaProducto]
    FOREIGN KEY ([MarcaId])
    REFERENCES [dbo].[Marcas]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_MarcaProducto'
CREATE INDEX [IX_FK_MarcaProducto]
ON [dbo].[Productos]
    ([MarcaId]);
GO

-- Creating foreign key on [SalonId] in table 'DetalleComprobantes_DetalleSalon'
ALTER TABLE [dbo].[DetalleComprobantes_DetalleSalon]
ADD CONSTRAINT [FK_SalonDetalleSalon]
    FOREIGN KEY ([SalonId])
    REFERENCES [dbo].[Comprobantes_Salon]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_SalonDetalleSalon'
CREATE INDEX [IX_FK_SalonDetalleSalon]
ON [dbo].[DetalleComprobantes_DetalleSalon]
    ([SalonId]);
GO

-- Creating foreign key on [ClienteId] in table 'Comprobantes_Delivery'
ALTER TABLE [dbo].[Comprobantes_Delivery]
ADD CONSTRAINT [FK_ClienteDelivery]
    FOREIGN KEY ([ClienteId])
    REFERENCES [dbo].[Personas_Cliente]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_ClienteDelivery'
CREATE INDEX [IX_FK_ClienteDelivery]
ON [dbo].[Comprobantes_Delivery]
    ([ClienteId]);
GO

-- Creating foreign key on [DeliveryId] in table 'DetalleComprobantes_DetalleDelivery'
ALTER TABLE [dbo].[DetalleComprobantes_DetalleDelivery]
ADD CONSTRAINT [FK_DeliveryDetalleDelivery]
    FOREIGN KEY ([DeliveryId])
    REFERENCES [dbo].[Comprobantes_Delivery]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_DeliveryDetalleDelivery'
CREATE INDEX [IX_FK_DeliveryDetalleDelivery]
ON [dbo].[DetalleComprobantes_DetalleDelivery]
    ([DeliveryId]);
GO

-- Creating foreign key on [CadeteId] in table 'Comprobantes_Delivery'
ALTER TABLE [dbo].[Comprobantes_Delivery]
ADD CONSTRAINT [FK_CadeteDelivery]
    FOREIGN KEY ([CadeteId])
    REFERENCES [dbo].[Personas_Empleado]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_CadeteDelivery'
CREATE INDEX [IX_FK_CadeteDelivery]
ON [dbo].[Comprobantes_Delivery]
    ([CadeteId]);
GO

-- Creating foreign key on [ListaPrecioId] in table 'Alicuotas'
ALTER TABLE [dbo].[Alicuotas]
ADD CONSTRAINT [FK_ListaPrecioAlicuota]
    FOREIGN KEY ([ListaPrecioId])
    REFERENCES [dbo].[ListaPrecios]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_ListaPrecioAlicuota'
CREATE INDEX [IX_FK_ListaPrecioAlicuota]
ON [dbo].[Alicuotas]
    ([ListaPrecioId]);
GO

-- Creating foreign key on [ListaPrecioId] in table 'ListaPrecioProductos'
ALTER TABLE [dbo].[ListaPrecioProductos]
ADD CONSTRAINT [FK_ListaPrecioListaPrecioProducto]
    FOREIGN KEY ([ListaPrecioId])
    REFERENCES [dbo].[ListaPrecios]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_ListaPrecioListaPrecioProducto'
CREATE INDEX [IX_FK_ListaPrecioListaPrecioProducto]
ON [dbo].[ListaPrecioProductos]
    ([ListaPrecioId]);
GO

-- Creating foreign key on [ProductoId] in table 'ListaPrecioProductos'
ALTER TABLE [dbo].[ListaPrecioProductos]
ADD CONSTRAINT [FK_ProductoListaPrecioProducto]
    FOREIGN KEY ([ProductoId])
    REFERENCES [dbo].[Productos]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_ProductoListaPrecioProducto'
CREATE INDEX [IX_FK_ProductoListaPrecioProducto]
ON [dbo].[ListaPrecioProductos]
    ([ProductoId]);
GO

-- Creating foreign key on [Proveedores_Id] in table 'ProveedorProducto'
ALTER TABLE [dbo].[ProveedorProducto]
ADD CONSTRAINT [FK_ProveedorProducto_Proveedor]
    FOREIGN KEY ([Proveedores_Id])
    REFERENCES [dbo].[Personas_Proveedor]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating foreign key on [Productos_Id] in table 'ProveedorProducto'
ALTER TABLE [dbo].[ProveedorProducto]
ADD CONSTRAINT [FK_ProveedorProducto_Producto]
    FOREIGN KEY ([Productos_Id])
    REFERENCES [dbo].[Productos]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_ProveedorProducto_Producto'
CREATE INDEX [IX_FK_ProveedorProducto_Producto]
ON [dbo].[ProveedorProducto]
    ([Productos_Id]);
GO

-- Creating foreign key on [Usuario_Id] in table 'Personas_Empleado'
ALTER TABLE [dbo].[Personas_Empleado]
ADD CONSTRAINT [FK_EmpleadoUsuario]
    FOREIGN KEY ([Usuario_Id])
    REFERENCES [dbo].[Usuarios]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_EmpleadoUsuario'
CREATE INDEX [IX_FK_EmpleadoUsuario]
ON [dbo].[Personas_Empleado]
    ([Usuario_Id]);
GO

-- Creating foreign key on [RubroId] in table 'Personas_Proveedor'
ALTER TABLE [dbo].[Personas_Proveedor]
ADD CONSTRAINT [FK_RubroProveedor]
    FOREIGN KEY ([RubroId])
    REFERENCES [dbo].[Rubros]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_RubroProveedor'
CREATE INDEX [IX_FK_RubroProveedor]
ON [dbo].[Personas_Proveedor]
    ([RubroId]);
GO

-- Creating foreign key on [CondicionIvaId] in table 'Personas_PersonaJuridica'
ALTER TABLE [dbo].[Personas_PersonaJuridica]
ADD CONSTRAINT [FK_CondicionIvaPersonaJuridica]
    FOREIGN KEY ([CondicionIvaId])
    REFERENCES [dbo].[CondicionIvas]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_CondicionIvaPersonaJuridica'
CREATE INDEX [IX_FK_CondicionIvaPersonaJuridica]
ON [dbo].[Personas_PersonaJuridica]
    ([CondicionIvaId]);
GO

-- Creating foreign key on [ClienteId] in table 'Comprobantes_Salon'
ALTER TABLE [dbo].[Comprobantes_Salon]
ADD CONSTRAINT [FK_ClienteSalon]
    FOREIGN KEY ([ClienteId])
    REFERENCES [dbo].[Personas_Cliente]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_ClienteSalon'
CREATE INDEX [IX_FK_ClienteSalon]
ON [dbo].[Comprobantes_Salon]
    ([ClienteId]);
GO

-- Creating foreign key on [MesaId] in table 'Comprobantes_Reserva'
ALTER TABLE [dbo].[Comprobantes_Reserva]
ADD CONSTRAINT [FK_MesaReserva]
    FOREIGN KEY ([MesaId])
    REFERENCES [dbo].[Mesas]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_MesaReserva'
CREATE INDEX [IX_FK_MesaReserva]
ON [dbo].[Comprobantes_Reserva]
    ([MesaId]);
GO

-- Creating foreign key on [Cliente_Id] in table 'CuentasCorriente'
ALTER TABLE [dbo].[CuentasCorriente]
ADD CONSTRAINT [FK_ClienteCuentaCorriente]
    FOREIGN KEY ([Cliente_Id])
    REFERENCES [dbo].[Personas_Cliente]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_ClienteCuentaCorriente'
CREATE INDEX [IX_FK_ClienteCuentaCorriente]
ON [dbo].[CuentasCorriente]
    ([Cliente_Id]);
GO

-- Creating foreign key on [EmpleadoId] in table 'Facturas'
ALTER TABLE [dbo].[Facturas]
ADD CONSTRAINT [FK_EmpleadoFactura]
    FOREIGN KEY ([EmpleadoId])
    REFERENCES [dbo].[Personas_Empleado]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_EmpleadoFactura'
CREATE INDEX [IX_FK_EmpleadoFactura]
ON [dbo].[Facturas]
    ([EmpleadoId]);
GO

-- Creating foreign key on [ClienteId] in table 'Facturas'
ALTER TABLE [dbo].[Facturas]
ADD CONSTRAINT [FK_ClienteFactura]
    FOREIGN KEY ([ClienteId])
    REFERENCES [dbo].[Personas_Cliente]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_ClienteFactura'
CREATE INDEX [IX_FK_ClienteFactura]
ON [dbo].[Facturas]
    ([ClienteId]);
GO

-- Creating foreign key on [CuentaCorrienteId] in table 'Facturas'
ALTER TABLE [dbo].[Facturas]
ADD CONSTRAINT [FK_CuentaCorrienteFactura]
    FOREIGN KEY ([CuentaCorrienteId])
    REFERENCES [dbo].[CuentasCorriente]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_CuentaCorrienteFactura'
CREATE INDEX [IX_FK_CuentaCorrienteFactura]
ON [dbo].[Facturas]
    ([CuentaCorrienteId]);
GO

-- Creating foreign key on [Comprobante_Id] in table 'Facturas'
ALTER TABLE [dbo].[Facturas]
ADD CONSTRAINT [FK_ComprobanteFactura]
    FOREIGN KEY ([Comprobante_Id])
    REFERENCES [dbo].[Comprobantes]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_ComprobanteFactura'
CREATE INDEX [IX_FK_ComprobanteFactura]
ON [dbo].[Facturas]
    ([Comprobante_Id]);
GO

-- Creating foreign key on [FacturaId] in table 'Movimientos'
ALTER TABLE [dbo].[Movimientos]
ADD CONSTRAINT [FK_FacturaMovimiento]
    FOREIGN KEY ([FacturaId])
    REFERENCES [dbo].[Facturas]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_FacturaMovimiento'
CREATE INDEX [IX_FK_FacturaMovimiento]
ON [dbo].[Movimientos]
    ([FacturaId]);
GO

-- Creating foreign key on [ClienteId] in table 'Movimientos'
ALTER TABLE [dbo].[Movimientos]
ADD CONSTRAINT [FK_ClienteMovimiento]
    FOREIGN KEY ([ClienteId])
    REFERENCES [dbo].[Personas_Cliente]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_ClienteMovimiento'
CREATE INDEX [IX_FK_ClienteMovimiento]
ON [dbo].[Movimientos]
    ([ClienteId]);
GO

-- Creating foreign key on [ProductoId] in table 'PedidosProducto'
ALTER TABLE [dbo].[PedidosProducto]
ADD CONSTRAINT [FK_ProductoPedidoProducto]
    FOREIGN KEY ([ProductoId])
    REFERENCES [dbo].[Productos]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_ProductoPedidoProducto'
CREATE INDEX [IX_FK_ProductoPedidoProducto]
ON [dbo].[PedidosProducto]
    ([ProductoId]);
GO

-- Creating foreign key on [ProveedorId] in table 'PedidosProducto'
ALTER TABLE [dbo].[PedidosProducto]
ADD CONSTRAINT [FK_ProveedorPedidoProducto]
    FOREIGN KEY ([ProveedorId])
    REFERENCES [dbo].[Personas_Proveedor]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_ProveedorPedidoProducto'
CREATE INDEX [IX_FK_ProveedorPedidoProducto]
ON [dbo].[PedidosProducto]
    ([ProveedorId]);
GO

-- Creating foreign key on [ProveedorId] in table 'Movimientos'
ALTER TABLE [dbo].[Movimientos]
ADD CONSTRAINT [FK_ProveedorMovimiento]
    FOREIGN KEY ([ProveedorId])
    REFERENCES [dbo].[Personas_Proveedor]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_ProveedorMovimiento'
CREATE INDEX [IX_FK_ProveedorMovimiento]
ON [dbo].[Movimientos]
    ([ProveedorId]);
GO

-- Creating foreign key on [Id] in table 'Personas_PersonaFisica'
ALTER TABLE [dbo].[Personas_PersonaFisica]
ADD CONSTRAINT [FK_PersonaFisica_inherits_Persona]
    FOREIGN KEY ([Id])
    REFERENCES [dbo].[Personas]
        ([Id])
    ON DELETE CASCADE ON UPDATE NO ACTION;
GO

-- Creating foreign key on [Id] in table 'Personas_Cliente'
ALTER TABLE [dbo].[Personas_Cliente]
ADD CONSTRAINT [FK_Cliente_inherits_PersonaFisica]
    FOREIGN KEY ([Id])
    REFERENCES [dbo].[Personas_PersonaFisica]
        ([Id])
    ON DELETE CASCADE ON UPDATE NO ACTION;
GO

-- Creating foreign key on [Id] in table 'Comprobantes_Reserva'
ALTER TABLE [dbo].[Comprobantes_Reserva]
ADD CONSTRAINT [FK_Reserva_inherits_Comprobante]
    FOREIGN KEY ([Id])
    REFERENCES [dbo].[Comprobantes]
        ([Id])
    ON DELETE CASCADE ON UPDATE NO ACTION;
GO

-- Creating foreign key on [Id] in table 'Personas_Empleado'
ALTER TABLE [dbo].[Personas_Empleado]
ADD CONSTRAINT [FK_Empleado_inherits_PersonaFisica]
    FOREIGN KEY ([Id])
    REFERENCES [dbo].[Personas_PersonaFisica]
        ([Id])
    ON DELETE CASCADE ON UPDATE NO ACTION;
GO

-- Creating foreign key on [Id] in table 'Comprobantes_Salon'
ALTER TABLE [dbo].[Comprobantes_Salon]
ADD CONSTRAINT [FK_Salon_inherits_Comprobante]
    FOREIGN KEY ([Id])
    REFERENCES [dbo].[Comprobantes]
        ([Id])
    ON DELETE CASCADE ON UPDATE NO ACTION;
GO

-- Creating foreign key on [Id] in table 'DetalleComprobantes_DetalleSalon'
ALTER TABLE [dbo].[DetalleComprobantes_DetalleSalon]
ADD CONSTRAINT [FK_DetalleSalon_inherits_DetalleComprobante]
    FOREIGN KEY ([Id])
    REFERENCES [dbo].[DetalleComprobantes]
        ([Id])
    ON DELETE CASCADE ON UPDATE NO ACTION;
GO

-- Creating foreign key on [Id] in table 'Comprobantes_Delivery'
ALTER TABLE [dbo].[Comprobantes_Delivery]
ADD CONSTRAINT [FK_Delivery_inherits_Comprobante]
    FOREIGN KEY ([Id])
    REFERENCES [dbo].[Comprobantes]
        ([Id])
    ON DELETE CASCADE ON UPDATE NO ACTION;
GO

-- Creating foreign key on [Id] in table 'DetalleComprobantes_DetalleDelivery'
ALTER TABLE [dbo].[DetalleComprobantes_DetalleDelivery]
ADD CONSTRAINT [FK_DetalleDelivery_inherits_DetalleComprobante]
    FOREIGN KEY ([Id])
    REFERENCES [dbo].[DetalleComprobantes]
        ([Id])
    ON DELETE CASCADE ON UPDATE NO ACTION;
GO

-- Creating foreign key on [Id] in table 'Personas_PersonaJuridica'
ALTER TABLE [dbo].[Personas_PersonaJuridica]
ADD CONSTRAINT [FK_PersonaJuridica_inherits_Persona]
    FOREIGN KEY ([Id])
    REFERENCES [dbo].[Personas]
        ([Id])
    ON DELETE CASCADE ON UPDATE NO ACTION;
GO

-- Creating foreign key on [Id] in table 'Personas_Proveedor'
ALTER TABLE [dbo].[Personas_Proveedor]
ADD CONSTRAINT [FK_Proveedor_inherits_PersonaJuridica]
    FOREIGN KEY ([Id])
    REFERENCES [dbo].[Personas_PersonaJuridica]
        ([Id])
    ON DELETE CASCADE ON UPDATE NO ACTION;
GO

-- --------------------------------------------------
-- Script has ended
-- --------------------------------------------------