try
{
    Console.Write("Введите количество сотрудников: ");
    int n = int.Parse(Console.ReadLine());
    Staff[] staff = new Staff[n];

    for (int i = 0; i < n; i++)
    {
        Console.Write("Введите фамилию сотрудника: ");
        staff[i].surname = Console.ReadLine();

        Console.Write("Введите имя сотрудника: ");
        staff[i].name = Console.ReadLine();

        Console.Write("Введите отчество сотрудника: ");
        staff[i].otchestvo = Console.ReadLine();

        Console.Write("Введите должность сотрудника: ");
        staff[i].job_title = Console.ReadLine();

        Console.Write("Введите зарплату сотрудника: ");
        staff[i].income = double.Parse(Console.ReadLine());

        Console.Write("Введите день рождения сотрудника в формате дд.мм.гггг (01.01.1000): ");
        staff[i].birthday = DateTime.Parse(Console.ReadLine());

        
        Console.WriteLine();
    }

    for (int i = 0; i < n; i++)
    {
        if (staff[i].birthday.Month == 5)
        {
            Console.WriteLine("Фамилия: " + staff[i].surname);
            Console.WriteLine("Имя: " + staff[i].name);
            Console.WriteLine("Отчество: " + staff[i].otchestvo);
            Console.WriteLine("Должность: " + staff[i].job_title);
            Console.WriteLine("Зарплата: " + staff[i].income);
            Console.WriteLine("Дата рождения: " + staff[i].birthday);
            Console.WriteLine();
        }
    }
}
catch(Exception ex)
{   Console.WriteLine(ex.Message);
}

struct Staff
{
    public string surname;
    public string name;
    public string otchestvo;
    public string job_title;
    public double income;
    public DateTime birthday;
   

}




