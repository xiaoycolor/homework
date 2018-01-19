using datasource;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace datasource
{
    class Program
    {
        const string ConnectionString = @"Data Source=(localdb)\mssqllocaldb;Initial Catalog=lesson01;Integrated Security=true;";
        static void Main(string[] args)
        {
            try
            {
                // 连接数据库引擎
                using (DataClasses1DataContext aDataContext = new DataClasses1DataContext(ConnectionString))
                {
                    if (!aDataContext.DatabaseExists())
                    {
                        aDataContext.CreateDatabase();
                        Console.WriteLine("数据库已经创建！");
                    }
                    else
                    {
                        Console.WriteLine("数据库已经存在！");
                    }

                    // 读取数据表内容
                    var aContacts = from r in aDataContext.lesson select r;
                    foreach (lesson aContact in aContacts)
                    {
                        Console.WriteLine($"{aContact.content} : {aContact.time}");
                    }

                    Console.WriteLine("插入新记录……");
                    lesson aNewContact = new lesson { content = "需要任何帮助请联系134****6666", time = DateTime.Now.ToString() };
                    aDataContext.lesson.InsertOnSubmit(aNewContact);

                    Console.WriteLine("删除记录……");
                    lesson aExistContact = (from r in aDataContext.lesson where r.content == "需要任何帮助请联系134****6666" select r).FirstOrDefault();
                    if (aExistContact != null)
                    {
                        aDataContext.lesson.DeleteOnSubmit(aExistContact);
                    }

                 

                    Console.WriteLine("提交数据……");
                    aDataContext.SubmitChanges();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                Console.WriteLine(ex.StackTrace);
            }

            Console.WriteLine("按回车键退出……");
            Console.ReadLine();
        }
    
}
}
