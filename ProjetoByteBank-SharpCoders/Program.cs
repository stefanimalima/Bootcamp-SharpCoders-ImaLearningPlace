using System.Collections.Generic;
using System;
using System.Linq;

namespace ByteBank1
{
    public class Program
    {
        public static void Main(string[] args)
        {
            int option;
            List<string> cpfs = new List<string>();
            List<string> titulares = new List<string>();
            List<string> senhas = new List<string>();
            List<double> saldos = new List<double>();

            Console.WriteLine("Bem vindo (a) ao ByteBank Sharp Coders!");

            do
            {
                MenuDoUsuario();
                option = int.Parse(Console.ReadLine());

                Console.WriteLine("---------------------------------------------------------");

                switch (option)
                {
                    case 0:
                        Console.WriteLine("O aplicativo está sendo encerrado...");
                        break;
                    case 1:
                        RegistrarNovoUsuario(cpfs, titulares, senhas, saldos);
                        break;
                    case 2:
                        DeletarUsuario(cpfs, titulares, senhas, saldos);
                        break;
                    case 3:
                        ListarTodasAsContas(cpfs, titulares, saldos);
                        break;
                    case 4:
                        MostrarUsuario(cpfs, titulares, saldos);
                        break;
                    case 5:
                        ApresentarValorAcumulado(saldos);
                        break;
                    case 6:
                        MenuDaConta(cpfs, titulares, senhas, saldos);
                        break;
                }

                Console.WriteLine("---------------------------------------------------------");

            } while (option != 0);
        }

        static void MenuDoUsuario()
        {
            Console.WriteLine("Digite o número desejado e aperte enter para continuar.");
            Console.WriteLine("---------------------------------------------------------");
            Console.WriteLine("| 1 | Registrar novo usuário                            |");
            Console.WriteLine("| 2 | Deletar usuário                                   |");
            Console.WriteLine("| 3 | Listar todas as contas registradas                |");
            Console.WriteLine("| 4 | Detalhes de um usuário                            |");
            Console.WriteLine("| 5 | Investimentos [Poupbank]                          |");
            Console.WriteLine("| 6 | ByteConta - Cliente                               |");
            Console.WriteLine("| 0 | Para sair do aplicativo                           |");
            Console.WriteLine("---------------------------------------------------------");
            Console.Write("Opção:");
        }

        static void MenuDaConta(List<string> cpfs, List<string> titulares, List<string> senhas, List<double> saldos)
        {
            Console.Write("Para acessar a conta, digite o seu CPF:");
            string cpf = Console.ReadLine();
            int option;

            if (ValidarUsuario(cpfs, senhas, titulares, cpf))
            {
                Console.WriteLine("Digite o número desejado e aperte enter para continuar.");
                Console.WriteLine("---------------------------------------------------------");
                Console.WriteLine("| 1 | Saque                                             |");
                Console.WriteLine("| 2 | Depósito[Adicionar fundos]                        |");
                Console.WriteLine("| 3 | Transferência bancária                            |");
                Console.WriteLine("| 0 | Voltar ao menu anterior                           |");
                Console.WriteLine("---------------------------------------------------------");
                Console.Write("Opção:");
                option = int.Parse(Console.ReadLine());
                Console.ReadKey();

                switch (option)
                {
                    case 0:
                        MenuDoUsuario();
                        break;
                    case 1:
                        Sacar(cpfs, saldos, cpf);
                        break;
                    case 2:
                        Depositar(cpfs, saldos, cpf);
                        break;
                    case 3:
                        Transferir(cpfs, titulares, saldos, cpf);
                        break;
                }
            }
            else
            {
                Console.WriteLine("Usuario não encontrado no banco de dados! Digite 0 para retornar.");
                Console.ReadLine();
                Console.ReadKey();
            }
        }

        private static void Transferir(List<string> cpfs, List<string> titulares, List<double> saldos, string cpf)
        {
            int index = cpfs.FindIndex(i => i.Equals(cpf));

            Console.Write("Digite o CPF para qual deseja transferir: ");
            string conta = Console.ReadLine();
            int indexTrasnferencia = cpfs.FindIndex(i => i == conta);

            if (indexTrasnferencia == -1)
            {
                Console.WriteLine("Esse cliente não possui uma ByteConta! Transferência não realizada.");
            }
            else
            {
                Console.Write($"Qual valor deseja tranferir para {titulares[indexTrasnferencia]}? ");
                double valorTransferencia = double.Parse(Console.ReadLine());

                if (valorTransferencia > saldos[index])
                {
                    Console.WriteLine("Saldo insuficiente! Transferência não realizada.");
                }
                else
                {
                    saldos[indexTrasnferencia] += valorTransferencia;
                    saldos[index] -= valorTransferencia;
                    Console.WriteLine("Transferência realizada com sucesso!");
                    Console.WriteLine($"Saldo atual: {saldos[index]}");
                }
            }
        }

        private static void Depositar(List<string> cpfs, List<double> saldos, string cpf)
        {
            int index = cpfs.FindIndex(i => i.Equals(cpf));

            Console.Write("Quanto deseja adicionar a sua conta? ");
            double valor = double.Parse(Console.ReadLine());

            saldos[index] += valor;
            Console.WriteLine("Deposito realizado com sucesso!");
            Console.WriteLine($"Saldo atual: {saldos[index]:C2}");

        }

        private static void Sacar(List<string> cpfs, List<double> saldos, string cpf)
        {
            int index = cpfs.FindIndex(i => i.Equals(cpf));

            Console.Write("Para concluir o saque, digite o valor desejado:");
            double valor = double.Parse(Console.ReadLine());

            if (valor >= saldos[index])
            {
                Console.WriteLine("Saldo insuficiente! Saque não realizado.");
            }
            else
            {
                saldos[index] -= valor;
                Console.WriteLine("Saque realizado com sucesso!");
                Console.WriteLine($"Saldo atual: {saldos[index]:C2}");
            }
        }

        private static bool ValidarUsuario(List<string> cpfs, List<string> senhas, List<string> titulares, string cpf)
        {
            int index = cpfs.FindIndex(i => i.Equals(cpf));

            if (index == -1)
                return false;

            Console.Write("Digite sua senha: ");
            string senha = Console.ReadLine();

            if (senha == senhas[index])
            {
                Console.WriteLine($"Seja bem vindo(a), {titulares[index]}");
                return true;
            }
            else
            {
                Console.WriteLine("Senha inválida!");
                return false;
            }
        }

        static void RegistrarNovoUsuario(List<string> cpfs, List<string> titulares, List<string> senhas, List<double> saldos)
        {
            Console.Write("Digite o CPF desejado: ");
            cpfs.Add(Console.ReadLine());
            Console.Write("Nome completo: ");
            titulares.Add(Console.ReadLine());
            Console.Write("Escolha uma senha: ");
            senhas.Add(Console.ReadLine());
            saldos.Add(0);
            Console.WriteLine("Cliente cadastrado (a) com sucesso!");
            Console.WriteLine("Digite 0 para retornar ao menu anterior.");
            Console.ReadLine();
        }

        static void DeletarUsuario(List<string> cpfs, List<string> titulares, List<string> senhas, List<double> saldos)
        {
            Console.Write("Digite o CPF desejado: ");
            string cpfParaDeletar = Console.ReadLine();
            int indexParaDeletar = cpfs.FindIndex(cpf => cpf == cpfParaDeletar);

            if (indexParaDeletar == -1)
            {
                Console.WriteLine("Não foi possível deletar esta conta!");
                Console.WriteLine("Erro 0547: Conta não encontrada no banco de dados.");
            }

            cpfs.Remove(cpfParaDeletar);
            titulares.RemoveAt(indexParaDeletar);
            senhas.RemoveAt(indexParaDeletar);
            saldos.RemoveAt(indexParaDeletar);

            Console.WriteLine("Sua conta foi deletada com sucesso!");
        }
        static void ListarTodasAsContas(List<string> cpfs, List<string> titulares, List<double> saldos)
        {
            for (int i = 0; i < cpfs.Count; i++)
            {
                MostrarConta(i, cpfs, titulares, saldos);
            }
        }

        static void MostrarUsuario(List<string> cpfs, List<string> titulares, List<double> saldos)
        {
            Console.Write("Digite o CPF desejado: ");
            string cpfParaMostrar = Console.ReadLine();
            int indexParaMostrar = cpfs.FindIndex(cpf => cpf == cpfParaMostrar);

            if (indexParaMostrar == -1)
            {
                Console.WriteLine("Não foi possível acessar esta conta!");
                Console.WriteLine("Erro 0548: Conta não encontrada no banco de dados.");
            }

            MostrarConta(indexParaMostrar, cpfs, titulares, saldos);
        }

        static void ApresentarValorAcumulado(List<double> saldos)
        {
            Console.WriteLine($"Total acumulado no banco: {saldos.Sum():C2}");
            
        }

        static void MostrarConta(int index, List<string> cpfs, List<string> titulares, List<double> saldos)
        {
            Console.WriteLine($"CPF = {cpfs[index]} | Titular = {titulares[index]} | Saldo = R${saldos[index]:C2}");
        }
    }
}