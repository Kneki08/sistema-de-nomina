using System.IO.Pipes;
using System.Runtime.CompilerServices;

public abstract class Empleado
{
    private string PrimerNombre;
    private string apellidoPaterno;
    private string numeroSeguroSocal;

    public Empleado(string Nombre, string Apllido, string NSS)
    {
        PrimerNombre = Nombre;
        apellidoPaterno = Apllido;
        numeroSeguroSocal = NSS;
    }
    public Empleado(string Apellidos,string NSS) 
    {
        apellidoPaterno = Apellidos;
        numeroSeguroSocal = NSS;
    }
    public virtual void Mostrar()
    {
        Console.WriteLine($"Nombre: {PrimerNombre} {apellidoPaterno}");
        Console.WriteLine($"NSS: {numeroSeguroSocal}");
    }
    public abstract decimal calcularSueldo();

}
class EmpleadoAsalariado : Empleado
{
    private decimal salariosemanal;
    public EmpleadoAsalariado(string NombreAsalariado, string ApllidoAsalariado, string NSSAsalariado, decimal semanal) : base(NombreAsalariado, ApllidoAsalariado, NSSAsalariado)
    {
        salariosemanal = semanal;
    }
    public void actualizarSalario(decimal nuevoSalario)
    {
        salariosemanal = nuevoSalario;
    }
    public override decimal calcularSueldo() => salariosemanal;
    public override void Mostrar()
    {
        base.Mostrar();
        Console.WriteLine($"salrio semanal: {salariosemanal}");
    }
}
class EmpleadoPorHora: Empleado
{
   private  decimal salarioPorHora;
    private int horasTrabajadas;

    public EmpleadoPorHora(string ApellidoPorHora,string NSSPorHora, decimal PorHora, int trabajosHoras) : base(ApellidoPorHora, NSSPorHora)
    {
        salarioPorHora = PorHora;
        horasTrabajadas = trabajosHoras;
    }
    public override decimal calcularSueldo()
    {
        if(horasTrabajadas <= 40)
        {
            return salarioPorHora * horasTrabajadas;
        }
        else
        {
            return (horasTrabajadas * 40) + ((horasTrabajadas - 40)*salarioPorHora * 1.5m);
        }
    }
    public void actualizarSalario(decimal nuevoPorHora,int nuevasHorasTrabajadas)
    {
        salarioPorHora = nuevoPorHora;
        horasTrabajadas = nuevasHorasTrabajadas;
    }
    public override void Mostrar()
    {
        base.Mostrar();
        Console.WriteLine($"Pago por hora: {salarioPorHora:C}");
        Console.WriteLine($"Horas trabajadas: {horasTrabajadas}");
        Console.WriteLine($"Salario calculado: {calcularSueldo():C}");

    }
}
class EmpleadoPorComision : Empleado // encargado del salario de los empleados por comision
{
    private decimal ventasBrutas;
    private decimal tarifaComision;
    public EmpleadoPorComision(string NombrePorComision,string apellidoPorComision, string NSSPorComision, decimal ventas, decimal  tarifa) : base(NombrePorComision,apellidoPorComision,NSSPorComision)
    {
        ventasBrutas = ventas;
        tarifaComision = tarifa;
    }
    public void actualizarSalario(decimal nuevasVentas, decimal nuevaTarifa)
    {
        ventasBrutas = nuevasVentas;
        tarifaComision += nuevaTarifa;
    }
    public override void Mostrar()
    {
        base.Mostrar();
        Console.WriteLine($"Ventas brutas: {ventasBrutas:C}");
        Console.WriteLine($"Tarifa de comisión: {tarifaComision:P}");
        Console.WriteLine($"Salario calculado: {calcularSueldo():C}");
    }
    public override decimal calcularSueldo() => ventasBrutas * tarifaComision;
}
class EmpleadoAsalariadoPorComision : EmpleadoPorComision//encargado del salario de los asalariado por comicion y tambien hacemos herencia con empleado por comision en vez de la clase empleado
{
    private decimal salarioBase;
    public EmpleadoAsalariadoPorComision(string NombreAsalariadoComision,string apellidoAsalariadoComision,string NSSAsalariadoComision,decimal ventas2, decimal tarifa2,decimal salarioBASE)
        : base(NombreAsalariadoComision,apellidoAsalariadoComision,NSSAsalariadoComision,ventas2,tarifa2) 
    {
        salarioBase = salarioBASE;
    }
    public void actualizarSalario(decimal nuevasVentas, decimal nuevaTarifa,decimal nuevoSalarioBase)
    {
        base.actualizarSalario(nuevasVentas, nuevaTarifa);
        salarioBase = nuevoSalarioBase;
    }
    public override void Mostrar()
    {
        base.Mostrar();
        Console.WriteLine($"Salario base: {salarioBase:C}");
        Console.WriteLine($"Salario total calculado: {calcularSueldo():C}");
    }
    public override decimal calcularSueldo()
    {
        return base.calcularSueldo() + salarioBase + (salarioBase * 0.10m);
    }
 
}
