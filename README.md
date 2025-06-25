# 🖥️ ZonaPC - Tienda de Componentes de PC

**ZonaPC** es una plataforma completa de e-commerce para la venta de hardware de computadoras. El sistema está dividido en dos grandes partes:

- 💻 **Frontend para usuarios finales (Blazor WebAssembly)**
- 🛠️ **Panel administrativo para gestión (WinForms .NET 9)**

La solución implementa buenas prácticas de desarrollo moderno: API RESTful con seguridad JWT, acceso a datos con Entity Framework Core y base de datos relacional en MySQL.

---

## 🚀 Tecnologías Utilizadas

- **Backend**: .NET 9, C#, Entity Framework Core, API RESTful
- **Base de datos**: MySQL
- **Frontend cliente**: Blazor WebAssembly + TailwindCSS
- **Frontend administrador**: Windows Forms (.NET 9)
- **Autenticación**: JWT (JSON Web Tokens)
- **Testing de API**: Swagger UI

---

## 🧩 Estructura del Proyecto

/TiendaComponentes
-- TiendaComponentes.API → API RESTful (.NET 9)
-- TiendaComponentes.Shared → DTOs y modelos compartidos
-- TiendaComponentes.Cliente → Frontend en Blazor WebAssembly
-- TiendaComponentes.Admin → Aplicación de escritorio (WinForms)

---

## 📦 Funcionalidades Implementadas

### Cliente Blazor (ZonaPC)
- ✅ Navegación por categorías y subcategorías (Notebooks, Placas de Video, etc.)
- ✅ Visualización de productos con imágenes, precios, y descuentos
- ✅ Registro y login de usuarios
- ✅ Carrito de compras persistente
- ✅ Finalización de compra y generación de pedido
- ✅ Visualización de pedidos propios con estado y detalles

### Panel Admin (WinForms)
- ✅ Gestión de productos: agregar, editar, eliminar
- ✅ Visualización y edición del estado de los pedidos (Pendiente, EnCamino, Entregado, Cancelado)
- ✅ Visualización de detalles de pedido con imágenes
- ✅ Filtros y herramientas administrativas

---

## 👨‍💻 Autores

- Agustín Degano  
- Valentín Terrádez  

Proyecto desarrollado para la materia **Seminario 1** - 2025  
Analista Universitario en Sistemas, UNR.
