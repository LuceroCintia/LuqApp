namespace Taller.Domain.Enums;

public enum EstadoPresupuesto { Pendiente = 1, Enviado = 2, Revision = 3, Aprobado = 4, Rechazado = 5 }
public enum EstadoTarea { Pendiente = 1, EnProceso = 2, Hecha = 3 }
public enum EstadoOrden { Creada = 1, EnProceso = 2, Finalizada = 3 }
public enum TipoSiniestro { Particular = 1, Seguro = 2 }
public enum RolUsuario { Admin = 1, Recepcionista = 2, JefeTaller = 3, Operario = 4 }
