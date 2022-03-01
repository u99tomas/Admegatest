namespace Shared.Constants.Permission
{
    public static class Permissions
    {
        public static class Users
        {
            public const string View = "Permisos.Usuarios.Ver";
            public const string Create = "Permisos.Usuarios.Crear";
            public const string Edit = "Permisos.Usuarios.Editar";
        }

        public static class Roles
        {
            public const string View = "Permisos.Roles.Ver";
            public const string Create = "Permisos.Roles.Crear";
            public const string Edit = "Permisos.Roles.Editar";
            private const string ManagePermissions = "Permisos.Roles.Administrar.Permisos";
        }
    }
}
