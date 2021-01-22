using FluentScheduler;
using System;

namespace Cshp_FluentScheduler
{
    class Program
    {
        static void Main(string[] args)
        {
            // Inicia o Agendador
            // Define instância do Registro
            JobManager.Initialize(new RegistroTarefasAgendadas());

            // Aguarda pela intervenção do usuário
            Console.WriteLine("Tecle algo...");
            Console.ReadLine();

            // Para o Agendador
            JobManager.StopAndBlock();  
        }
    }

    public class RegistroTarefasAgendadas: Registry
    {
        public RegistroTarefasAgendadas()
        {
            Schedule<MeuJob>()
            .NonReentrant() //Permite uma instância de Job por vez
            .ToRunOnceAt(DateTime.Now.AddSeconds(2)) //Aguarda 2 segundos
            .AndEvery(5).Seconds(); //Intervalo de 5 segundos

            // Agenda um job para ser executado em uma hora e dia específico
            Schedule(() => Console.WriteLine("Agora são 21:00, dia 21 de Janeiro ")).ToRunEvery(1).Days().At(21, 00);
        }
    }

    public class MeuJob: IJob
    {
        public void Execute()
        {
            Console.WriteLine($"São {DateTime.Now}");
        }
    }
}
