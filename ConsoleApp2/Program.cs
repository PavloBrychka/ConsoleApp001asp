using Autofac;
using Autofac.Builder;

public class Data
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; } 

}
public interface IControlerWriteFile
{
    public void Writer(List<Data> koktels);

}
public interface IControlerPrintConsole
{
    public void Print(List<Data> koktels);
}
public interface IControlerAddData
{
 
    public void AddData(Data koktel);

    public List<Data> GetData();
}

public class ControlerWriteFile : IControlerWriteFile
{
    public void Writer(List<Data> koktels)
    {

        using(StreamWriter writer = new StreamWriter("temp.txt"))
        {
            foreach (Data data in koktels)
            {
                writer.WriteLine(data.Id + ")." + data.Name + " - \n" + data.Description);
            }
        }
       
       
    }
}

public class ControlerPrintConsole : IControlerPrintConsole
{
    public void Print(List<Data> koktels)
    {
       foreach(Data data in koktels)
        {
            Console.WriteLine(data.Id + ")." + data.Name + " - \n" + data.Description);
        }
    }
}

public class ControlerAddData : IControlerAddData
{
    private List<Data> koktels;
    public ControlerAddData()
    {
        koktels = new List<Data>();
    }
    public void AddData(Data koktel)
    {
        int id = 0;
        if(koktels.Count == 0)
        {
            id = 1;
        }
        else
        {
            id = koktels.Count;
        }
        koktel.Id = id;
        koktels.Add(koktel);
    }

    public List<Data> GetData()
    {
       return koktels;
    }

   
}

public class Program
{
    private static IContainer Container { get; set; }

    static void Main(string[] args)
    {
        var builder = new ContainerBuilder();
        builder.RegisterType<ControlerPrintConsole>().As<IControlerPrintConsole>();
        builder.RegisterType<ControlerWriteFile>().As<IControlerWriteFile>();
        builder.RegisterType<ControlerAddData>().As<IControlerAddData>();
        Container = builder.Build();

        List<Data> koktels;
        var getdata = Container.BeginLifetimeScope();
        var addwriter = getdata.Resolve<IControlerAddData>();
        var printwriter = getdata.Resolve<IControlerPrintConsole>();
        var writer = getdata.Resolve<IControlerWriteFile>();

        while (true)
        {
            Console.WriteLine("0 - Exit");
            Console.WriteLine("1 - Menu");
            Console.WriteLine("2 - Add Koktel");
            Console.WriteLine("3 - Print Console");
            Console.WriteLine("4 - Write File");
            Console.Write("Enter Data__ ");
            int x = Convert.ToInt32(Console.ReadLine());
            Console.Clear();
            if (x == 0)
            {
                Console.Clear();
                Console.WriteLine("Goodbaye!");
                break;
            }
            else if(x == 2)
            {
                Data temp = new Data();

                Console.WriteLine("Enter Name Koktel");
                temp.Name = Console.ReadLine().ToString();
                Console.WriteLine("Enter Recept Koktel");
                temp.Description = Console.ReadLine().ToString();

                
               
                   
                    addwriter.AddData(temp);
                    Console.WriteLine("Success");
                    Console.ReadKey();
                



            }
            else if(x == 3)
            {
                
                
                    
                    koktels = addwriter.GetData();
                
                if (koktels != null)
                {
              
                        printwriter.Print(koktels);
                        Console.WriteLine("Success");
                        Console.ReadKey();
                  
                }
               
            }
            else if(x == 4)
            {
               
                    
                    koktels = addwriter.GetData();
                
                if (koktels != null)
                {
                    
                        
                        writer.Writer(koktels);
                        Console.WriteLine("Success");
                        Console.ReadKey();
                    
                }
            }
            else
            {
                Console.WriteLine("ERROR!");
                x = 1;
                continue;
            }
        }
    }
}

