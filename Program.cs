using System;
using System.Collections.Generic;
using System.Linq;

namespace _net
{
    class Program
    {
        static void Main(string[] args)
        {
            //Program1();
            Program5();
        }

        static void Program1()
        {
            int yearActual = DateTime.Now.Year;
            Console.Write("Digite o ano de nascimento: ");            
            if (int.TryParse(Console.ReadLine(), out int yearBirthday))
            {                
                Console.WriteLine("A idade da pessoa é: {0}", yearActual - yearBirthday);
                Console.WriteLine("A idade da pessoa em 2035 será: {0}", 2035 - yearBirthday);
            }
        }

        static void Program2()
        {
            decimal newSalary = 0;
            Console.Write("Digite o seu salário: ");            
            if (decimal.TryParse(Console.ReadLine(), out decimal salary))
            {                
                System.Func<decimal, decimal, decimal> calculate = (a, b) => a + (a * b);
                if (salary > 0)
                {
                    decimal increase = (salary <= 500) ? 0.2M : 0.1M;
                    newSalary = calculate(salary, increase);                     
                    Console.WriteLine("O salário atual é: {0:C}", salary);
                    Console.WriteLine("Com o acrescimento de {0:0}% o salário novo é: {1:C}", increase * 100 , newSalary);
                }
            }
        }

        static void Program3()
        {
            int count = 0;
            Console.Write("Digite o número: ");            
            if (int.TryParse(Console.ReadLine(), out int number))
            {                
                if (number > 1)
                {
                    for (int i = 2; i <= number; i++)
                    {
                        if (number % i == 0) 
                        {
                            count ++;
                        }
                    }
                    Console.WriteLine("O número {0} {1} primo", count > 1 ? "não é" : "é", number);
                } 
                else
                {
                    Console.WriteLine("Digite o número maior que 1");
                }
            }
        }

        static void Program4()
        {
            int x, y, z = 0;
            void showError() => throw new System.Exception("number inválid");
            try
            {
                Console.WriteLine("    Digite os valores de um Triângulo");
                Console.WriteLine("--------------------------------------------");

                Console.Write(" Digite o número de X: ");
                if (!int.TryParse(Console.ReadLine(), out x))
                {
                    showError();
                }

                Console.Write(" Digite o número de Y: ");
                if (!int.TryParse(Console.ReadLine(), out y))
                {
                    showError();
                }

                Console.Write(" Digite o número de Z: ");
                if (!int.TryParse(Console.ReadLine(), out z))
                {
                    showError();
                }    

                Console.WriteLine(" Triângulo de X: {0} - Y: {1} - Z: {2}", x, y, z);

                if (x < (y + z) && y < (x + z) && z < (x + y))
                {
                    if (x == y && y == z) 
                    {
                        Console.Write(" O triângulo digitado é Equilátero");
                    }
                    else if (x != y && x != z && y != z) 
                    {
                        Console.Write(" O triângulo digitado é Escaleno");
                    }
                    else if (x == y || x == z || y == z) 
                    {
                        Console.Write(" O triângulo digitado é Isósceles");
                    }
                }
                else
                {
                    Console.Write(" Não é um triângulo");
                }
            }
            catch (System.Exception)
            {                
                throw;
            }
            
        }


        static void Program5()
        {                        
            EmployeeList list = new EmployeeList();
            int i = 0;
            string name = string.Empty;
            int[] point = new int[3];
            Console.WriteLine(" Sistema de Funcionários ");
            Console.WriteLine("****************************************************************************");
            while (i < 2)
            {            
                Console.Write("Digite o nome: ");
                name = Console.ReadLine();
                Console.Write("Digite o mês de Novembro: ");
                point[0] = int.Parse(Console.ReadLine());
                Console.Write("Digite o mês de Dezembro: ");
                point[1] = int.Parse(Console.ReadLine());
                Console.Write("Digite o mês de Janeiro: ");
                point[2] = int.Parse(Console.ReadLine());
                Accumulate ac0 = new Accumulate("Novembro", point[0]);
                Accumulate ac1 = new Accumulate("Dezembro", point[1]);
                Accumulate ac2 = new Accumulate("Janeiro", point[2]);
                Employee employee = new Employee(name, new List<Accumulate> {ac0, ac1, ac2});
                list.Add(employee);
                i++;                
            }
            GlobalSummaryEmployee(list);
            void GlobalSummaryEmployee(List<Employee> item)
            {
                Console.WriteLine("");
                Console.WriteLine(" Soma Geral de Pontos ");
                Console.WriteLine("****************************************************************************");
                Console.WriteLine("Nome Completo                    Soma                         Média         ");
                Console.WriteLine("****************************************************************************");
                item.ForEach(x => {
                    Console.WriteLine("{0} {1} {2:n}", 
                        x.Name.PadRight(34,' '), 
                        x.Points.Sum(x => x.Point).ToString().PadRight(26, ' '), 
                        x.Points.Average(x => x.Point)
                    );
                });

                Console.WriteLine("");
                Console.WriteLine(" Pontuação por Mês / Funcionários ");
                Console.WriteLine("****************************************************************************");
                Console.WriteLine("Nome Completo                    Mês                         Valor          ");
                Console.WriteLine("****************************************************************************");
                item.Select(x => new {
                    Name = x.Name, 
                    Point = x.Points.Max(x => x.Point),
                    Month = x.Points.Where(b => b.Point == x.Points.Max(c => c.Point))
                        .Select(x => x.Month)
                        .FirstOrDefault()
                })
                .ToList()
                .ForEach(x => {
                    Console.WriteLine("{0} {1} {2}", 
                        x.Name.PadRight(34,' '), 
                        x.Month.ToString().PadRight(26, ' '),
                        x.Point
                    );
                });
            }
        }
    }
    class Accumulate 
    {
        public Accumulate(string month, int point)
        {
            Month = month;
            Point = point;
        }
        public string Month { get; set; }
        public int Point { get; set; }
    }

    class Employee 
    {
        public Employee(string name, List<Accumulate> points)
        {
            Id = Guid.NewGuid();
            Name = name;
            Points = points;
        }
        public Guid Id { get; set; }
        public string Name { get; set; }
        public List<Accumulate> Points { get; set; }
    }

    class EmployeeList : List<Employee> {}
}
