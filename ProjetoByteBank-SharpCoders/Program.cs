using System.Collections.Generic;
using System;
using System.Linq;

namespace ByteBank1
{

    public class Program
    {

        static void menuDoUsuario()
        {
            Console.WriteLine("1 - Inserir novo usuário");
            Console.WriteLine("2 - Deletar um usuário");
            Console.WriteLine("3 - Listar todas as contas registradas");
            Console.WriteLine("4 - Detalhes de um usuário");
            Console.WriteLine("5 - Quantia armazenada no banco");
            Console.WriteLine("6 - Manipular a conta");
            Console.WriteLine("0 - Para sair do programa");
            Console.Write("Digite a opção desejada: ");
        }

        static void registrarNovoUsuario(List<string> cpfs, List<string> titulares, List<string> senhas, List<double> saldos)
        {
            Console.Write("Digite o CPF desejado: ");
            cpfs.Add(Console.ReadLine());
            Console.Write("Digite o seu nome: ");
            titulares.Add(Console.ReadLine());
            Console.Write("Digite a sua senha: ");
            senhas.Add(Console.ReadLine());
            saldos.Add(0);
        }

        static void deletarUsuario(List<string> cpfs, List<string> titulares, List<string> senhas, List<double> saldos)
        {
            Console.Write("Digite o CPF desejado: ");
            string cpfParaDeletar = Console.ReadLine();
            int indexParaDeletar = cpfs.FindIndex(cpf => cpf == cpfParaDeletar);

            if (indexParaDeletar == -1)
            {
                Console.WriteLine("Não foi possível deletar esta conta!");
                Console.WriteLine("MOTIVO: Conta não encontrada.");
            }

            cpfs.Remove(cpfParaDeletar);
            titulares.RemoveAt(indexParaDeletar);
            senhas.RemoveAt(indexParaDeletar);
            saldos.RemoveAt(indexParaDeletar);

            Console.WriteLine("Sua conta foi deletada com sucesso!");
        }

        static void listarTodasAsContas(List<string> cpfs, List<string> titulares, List<double> saldos)
        {
            for (int i = 0; i < cpfs.Count; i++)
            {
                apresentaConta(i, cpfs, titulares, saldos);
            }
        }

        static void apresentarUsuario(List<string> cpfs, List<string> titulares, List<double> saldos)
        {
            Console.Write("Digite o CPF desejado: ");
            string cpfParaApresentar = Console.ReadLine();
            int indexParaApresentar = cpfs.FindIndex(cpf => cpf == cpfParaApresentar);

            if (indexParaApresentar == -1)
            {
                Console.WriteLine("Não foi possível acessar esta conta.");
                Console.WriteLine("MOTIVO: Conta não encontrada.");
            }

            apresentaConta(indexParaApresentar, cpfs, titulares, saldos);
        }

        static void ApresentarValorAcumulado(List<double> saldos)
        {
            Console.WriteLine($"Total acumulado no banco: {saldos.Sum()}");
            // saldos.Sum(); ou .Agregatte(0.0, (x, y) => x + y)
        }

        static void apresentaConta(int index, List<string> cpfs, List<string> titulares, List<double> saldos)
        {
            Console.WriteLine($"CPF = {cpfs[index]} | Titular = {titulares[index]} | Saldo = R${saldos[index]:F2}");
        }

        public static void Main(string[] args)
        {

            Console.WriteLine("Antes de começar a utilizar, será necessário realizar algumas configurações: ");

            List<string> cpfs = new List<string>();
            List<string> titulares = new List<string>();
            List<string> senhas = new List<string>();
            List<double> saldos = new List<double>();

            int option;

            do
            {
                menuDoUsuario();
                option = int.Parse(Console.ReadLine());

                Console.WriteLine("-----------------");

                switch (option)
                {
                    case 0:
                        Console.WriteLine("O aplicativo está sendo encerrado...");
                        break;
                    case 1:
                        registrarNovoUsuario(cpfs, titulares, senhas, saldos);
                        break;
                    case 2:
                        deletarUsuario(cpfs, titulares, senhas, saldos);
                        break;
                    case 3:
                        listarTodasAsContas(cpfs, titulares, saldos);
                        break;
                    case 4:
                        apresentarUsuario(cpfs, titulares, saldos);
                        break;
                }

                Console.WriteLine("-----------------");

            } while (option != 0);



        }

    }

}
