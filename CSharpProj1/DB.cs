using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.Xml.Serialization;
using System.IO;
using System.Runtime.Serialization;
using System.Xml;

namespace Lab3
{
    [DataContract]
    public class DB_Item<TKey,TValue>
    {
        [DataMember]
        public TKey Key { get; set; }

        [DataMember]
        public TValue Value1 { get; set; }
        
        [DataMember]
        public TValue Value2 { get; set; }


        
        public DB_Item() 
        {
           
        }

       
        public DB_Item(TKey key, TValue val1, TValue val2)
        {
            
            if (key == null)
            {
                throw new ArgumentNullException(nameof(key));
            }

            if (val1 == null)
            {
                throw new ArgumentNullException(nameof(val1));
            }
            if (val2 == null)
            {
                throw new ArgumentNullException(nameof(val2));
            }

            Key = key;
            Value1 = val1;
            Value2 = val2;
        }


    }


    [DataContract]
    public class DataBase<TKey, TValue>  
    {

        [DataMember]
        private List<DB_Item<TKey, TValue>> items = new List<DB_Item<TKey, TValue>>();

        private static DataBase<TKey, TValue> instance;

        private DataBase()
        {

        }


        public static DataBase<TKey, TValue> init()
        {
            if (instance == null)
            {
                instance = new DataBase<TKey, TValue>();
            }
            return instance;
        }


        public void sort(int a)
        {
            switch (a)
            {
                case 1:
                    items = items.OrderBy(item => item.Key).ToList();
                    break;
                case 2:
                    items = items.OrderBy(item => item.Value1).ToList();
                    break;
                case 3:
                    items = items.OrderBy(item => item.Value2).ToList();
                    break;
                default:
                    break;
            }
        }


        public void serialize(string filename)
        {
            if (!File.Exists(filename))
            {
                throw new FileNotFoundException(nameof(filename));
            }

            XmlTextWriter xw = new XmlTextWriter(filename, Encoding.UTF8);
            xw.Formatting = Formatting.Indented;
            XmlDictionaryWriter writer = XmlDictionaryWriter.CreateDictionaryWriter(xw);
            DataContractSerializer ser = new DataContractSerializer(typeof(DataBase<TKey, TValue>));
            ser.WriteObject(writer, this);
            writer.Close();
            xw.Close();
            Console.WriteLine("Serialization complete!");
        }


        public DataBase<TKey, TValue> deserialize(string filename)
        {
            if (!File.Exists(filename))
            {
                throw new FileNotFoundException(nameof(filename));
            }
            
             DataBase<TKey, TValue> msc = null; 
            using (FileStream fs = new FileStream(filename, FileMode.Open))
            {
                XmlDictionaryReader reader = XmlDictionaryReader.CreateTextReader(fs, Encoding.UTF8, new XmlDictionaryReaderQuotas(), null);
                DataContractSerializer ser = new DataContractSerializer(typeof(DataBase<TKey, TValue>));
                msc = (DataBase<TKey, TValue>)ser.ReadObject(reader);
            }

            Console.WriteLine("Deserialization complete!");
            return msc;
        }
        

        
        public void Add(TKey key, TValue val1, TValue val2)
        {
           
            if (key == null)
            {
                throw new ArgumentNullException(nameof(key));
            }
            if (val1 == null)
            {
                throw new ArgumentNullException(nameof(val1));
            }
            if(val2 == null)
            {
                throw new ArgumentNullException(nameof(val2));
            }

           

            if (items.Any(i => i.Key.Equals(key)))
            {
                throw new ArgumentException($"Item exists with key {key}.", nameof(key));
            }

            
            var item = new DB_Item<TKey, TValue>()
            {
                Key = key,
                Value1 = val1,
                Value2 = val2
            };

            items.Add(item);
        }


        
        public void Remove(TKey key)
        {
            if (key == null)
            {
                throw new ArgumentNullException(nameof(key));
            }

            var item = items.SingleOrDefault(i => i.Key.Equals(key));

            if (item != null)
            {
                items.Remove(item);
            }
        }



        
        public void Update(TKey key, TValue newValue1, TValue newValue2)
        {
            if (key == null)
            {
                throw new ArgumentNullException(nameof(key));
            }
            if (newValue1 == null)
            {
                throw new ArgumentNullException(nameof(newValue1));
            }
            if (newValue2 == null)
            {
                throw new ArgumentNullException(nameof(newValue2));
            }
            if (!items.Any(i => i.Key.Equals(key)))
            {
                throw new ArgumentException($"Словарь не содержит значение с ключом {key}.", nameof(key));
            }

            var item = items.SingleOrDefault(i => i.Key.Equals(key));

            if (item != null)
            {
                item.Value1 = newValue1;
                item.Value2 = newValue2;
            }
        }


        public void show()
        {
            Console.WriteLine("     Phone Book Primitive Data Base");
            Console.WriteLine("+------------------------------------------+");
            Console.WriteLine("|    id    |      Name    |       Phone    |");
            Console.WriteLine("+------------------------------------------+");

            foreach (DB_Item<TKey,TValue> a in items)
            {
                Console.WriteLine("| " + a.Key + " | " + a.Value1 + " | " + a.Value2 + " | ");
            }
            Console.WriteLine("+------------------------------------------+");
        }

    }




    public class DataBaseController
    {


        private static DataBaseController instance;

        private DataBaseController()
        {

        }


        public static DataBaseController init()
        {
            if (instance == null)
            {
                instance = new DataBaseController();
            }
            return instance;
        }


        public void start()
        {
            DataBase<string, string> db = DataBase<string, string>.init();

            Console.WriteLine("+------------------------------------------------------------+");
            Console.WriteLine("|         I am primitive data base contole interface         |");
            Console.WriteLine("|     1 - add new data; 2 - delete data; 3 - show data;      |");
            Console.WriteLine("| 4 - update data; 5 - serialize data; 6 - deserialize data; |");
            Console.WriteLine("+------------------------------------------------------------+");

            string comand = "";


            string choice = " ";

            do
            {
                Console.WriteLine("#----Make your choice----#");

                comand = Console.ReadLine();

                switch (Int32.Parse(comand))
                {
                    case 1:
                        Console.Write("Input id: ");
                        string add_key = Console.ReadLine();
                        Console.Write("Input name: ");
                        string add_val1 = Console.ReadLine();
                        Console.Write("Input phone: ");
                        string add_val2 = Console.ReadLine();
                        db.Add(add_key, add_val1, add_val2);
                        break;
                    case 2:
                        Console.Write("Input id to delete: ");
                        string delete_key = Console.ReadLine();
                        db.Remove(delete_key);
                        break;
                    case 3:
                        db.show();
                        break;
                    case 4:
                        Console.Write("Input id: ");
                        string update_key = Console.ReadLine();
                        Console.Write("Input name: ");
                        string update_val1 = Console.ReadLine();
                        Console.Write("Input phone: ");
                        string update_val2 = Console.ReadLine();
                        db.Update(update_key, update_val1, update_val2);
                        break;
                    case 5:
                        Console.Write("Input file for serialization: ");
                        string filename = Console.ReadLine();
                        db.serialize(filename);
                        break;
                    case 6:
                        
                        break;
                    default:
                        break;
                }


                Console.Write("+-----Continue?(yes/no)-----+");
                choice = Console.ReadLine();

                if (choice == "no")
                    break;
            }
            while (true);


                Console.WriteLine("THE END!!!");
        }



    }


}
