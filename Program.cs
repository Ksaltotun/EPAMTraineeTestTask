using System;
using System.Collections;
using System.Collections.Generic;

namespace testTask
{

    class Company
    {
        private List<Client> clients = new List<Client>();
        private int totalSum = 0;
        private int baseTax = 15;
        private int totalSumUsual = 0;
        private int totalPrivilegeSum = 0;
        public void addClient(Client client)
        {
            clients.Add(client);
        }

        public void sortByEnergyConsuming()
        {
            Console.WriteLine("\nОтсортированны по потреблению по убыванию: \n");
            clients.Sort(new ByEnergyConsuming());
            foreach (Client client in clients)
            {
                Console.WriteLine($"Имя клиента: {client.clientName}");
                Console.WriteLine($"Потреблено электроэнергии: {client.clientEnergyConsuming}\n");
            }
        }
        public void sortByPayment()
        {
            Console.WriteLine("\nОтсортированны по оплате по возрастанию: \n");
            clients.Sort(new ByClientPayment());
            foreach (Client client in clients)
            {
                Console.WriteLine($"Имя клиента: {client.clientName}");
                Console.WriteLine($"Потреблено: {client.clientEnergyConsuming}, Оплачено: {client.clientPayment}\n");
            }
        }
        public void sortByClientType()
        {
            Console.WriteLine("\nОтсортированны по типу клиентов: \n ");
            clients.Sort(new ByClientType());
            foreach (Client client in clients)
            {
                Console.WriteLine($"Имя клиента: {client.clientName}, тип тарифа: {client.clientRateType} \n");
            }
        }

        public void calculateTotalSum()
        {
            foreach (Client client in clients)
            {
                totalSum += client.clientPayment;
                totalSumUsual += client.clientEnergyConsuming * baseTax;
            }

            Console.WriteLine($"\nОбщая сумма всех клиентов за потребленную электроэнергию {totalSum}");
        }

        public void calculateTotalSumPrivilege()
        {
            totalPrivilegeSum = totalSumUsual - totalSum;
            Console.Write($"\nОбщий размер льготы:  {totalPrivilegeSum}\n");
        }
    }

    class Client
    {
        public string clientName;
        public string clientRateType;
        public int clientEnergyConsuming;
        public int clientPayment;
        public int clientType;
        private int baseTax = 15;
        private int overLimitTax = 20;
        private int energyLimit = 150;
        private int privilegeOneTax = 10;
        private int privilegeTwoLimit = 50;
        public void calcClientPayment()
        {

            switch (clientRateType)
            {
                case "обычный":
                    clientPayment = clientEnergyConsuming * baseTax;
                    clientType = 0;
                    break;
                case "с лимитом":
                    clientType = 1;
                    if (clientEnergyConsuming > energyLimit)
                    {
                        clientPayment = energyLimit * baseTax + (clientEnergyConsuming - energyLimit) * overLimitTax;
                    }
                    else
                    {
                        clientPayment = clientEnergyConsuming * baseTax;
                    }
                    break;
                case "льготный №1":
                    clientType = 2;
                    clientPayment = clientEnergyConsuming * privilegeOneTax;
                    break;
                case "льготный №2":
                    clientType = 3;
                    if (clientEnergyConsuming > privilegeTwoLimit)
                    {
                        clientPayment = (clientEnergyConsuming - privilegeTwoLimit) * baseTax;
                    }
                    break;
                default:
                    break;
            }
        }
    }

    class ByEnergyConsuming : IComparer<Client>
    {
        public int Compare(Client object1, Client object2)
        {
            if (object1.clientEnergyConsuming > object2.clientEnergyConsuming)
            {
                return -1;
            }
            else if (object1.clientEnergyConsuming < object2.clientEnergyConsuming)
            {
                return 1;
            }
            else
            {
                return 0;
            }

        }
    }
    class ByClientPayment : IComparer<Client>
    {
        public int Compare(Client object1, Client object2)
        {
            if (object1.clientPayment > object2.clientPayment)
            {
                return 1;
            }
            else if (object1.clientPayment < object2.clientPayment)
            {
                return -1;
            }
            else
            {
                return 0;
            }

        }
    }

    class ByClientType : IComparer<Client>
    {
        public int Compare(Client object1, Client object2)
        {
            if (object1.clientType > object2.clientType)
            {
                return 1;
            }
            else if (object1.clientType < object2.clientType)
            {
                return -1;
            }
            else
            {
                return 0;
            }

        }
    }

    class Program
    {
        static void generateClients(int usualClientCount = 3, int limitClientCount = 3, int privilegeOneClientCount = 3, int privilegeTwoClientCount = 3)
        {
            string[] names = new string[] { "Иван", "Петр", "Николай", "Елена", "Мария", "Наталья" };
            string[] surnames = new string[] { "Петренко", "Сидоренко", "Николаенко", "Мариенко", "Еленко", "Натальенко" };
            Console.WriteLine("Поехали!");
            Company GSPK666 = new Company();

            Random random = new Random();
            for (int i = 0; i < usualClientCount; i++)
            {
                Client client = new Client();
                client.clientEnergyConsuming = random.Next(0, 200);
                client.clientName = names[random.Next(0, 5)] + " " + surnames[random.Next(0, 5)];
                client.clientRateType = "обычный";
                client.calcClientPayment();
                GSPK666.addClient(client);
            }
            for (int i = 0; i < limitClientCount; i++)
            {
                Client client = new Client();
                client.clientEnergyConsuming = random.Next(0, 200);
                client.clientName = names[random.Next(0, 5)] + " " + surnames[random.Next(0, 5)];
                client.clientRateType = "с лимитом";
                client.calcClientPayment();
                GSPK666.addClient(client);
            }
            for (int i = 0; i < privilegeOneClientCount; i++)
            {
                Client client = new Client();
                client.clientEnergyConsuming = random.Next(0, 200);
                client.clientName = names[random.Next(0, 5)] + " " + surnames[random.Next(0, 5)];
                client.clientRateType = "льготный №1";
                client.calcClientPayment();
                GSPK666.addClient(client);
            }
            for (int i = 0; i < privilegeTwoClientCount; i++)
            {
                Client client = new Client();
                client.clientEnergyConsuming = random.Next(0, 200);
                client.clientName = names[random.Next(0, 5)] + " " + surnames[random.Next(0, 5)];
                client.clientRateType = "льготный №2";
                client.calcClientPayment();
                GSPK666.addClient(client);
            }

            GSPK666.sortByEnergyConsuming();
            GSPK666.sortByPayment();
            GSPK666.calculateTotalSum();
            GSPK666.calculateTotalSumPrivilege();
            GSPK666.sortByClientType();
        }
        static void Main(string[] args)
        {
            generateClients();
            Console.ReadKey();
        }
    }
}


