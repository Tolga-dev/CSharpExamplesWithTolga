
using Query.From.E1;
using Query.Group.E1;
using Query.Group.E2;

namespace Query
{
    namespace From
    {
        namespace E1
        {
            class From
            {
                private int[] ids = { 0, 1, 2, 3, 4 };
                private IEnumerable<int> CreateQuery()
                {
                    var low = from i in ids where i < 4 select i;
                    return low;
                }
                public void ExecuteQuery()
                {
                    foreach(int i in CreateQuery())
                       Console.Write(i + " ");
                }

            }
        }

        namespace E2
        {
            public class Data
            {
                public string subject { get; set;}
                public List<int>  points { get; set;}
            }

            public class Query
            {
                private List<Data> _datas = new List<Data>()
                {
                    new Data { subject = "A", points = new List<int> { 1, 2, 3, 4, 5, 6 } },
                    new Data { subject = "B", points = new List<int> { 1, 2, 3, 4, 5, 6 } },
                    new Data { subject = "C", points = new List<int> { 1, 2, 3, 4, 5, 6 } },
                    new Data { subject = "D", points = new List<int> { 1, 2, 3, 4, 5, 6,7 } },
                };

                public void Create()
                {
                    var que = from sub in _datas
                        from score in sub.points
                        where score > 6
                        select new { Last = sub.subject, score };

                    foreach (var student in que)
                    {
                        Console.WriteLine("{0} Score: {1}", student.Last, student.score);
                    }
                }
            }
            
            
        }
        
    }

    namespace Group
    {
        namespace E1
        {
            public class st
            {
                public int id { get; set; }
                public List<int> pts;
            }
            class Group
            {
                private List<st> _cool_list = new List<st>
                {
                    new st {id = 0, pts = new List<int>{0,1,2,3}},
                    new st {id = 1, pts = new List<int>{0,1,2,3}},
                    new st {id = 2, pts = new List<int>{1,2,3}},
                    new st {id = 3, pts = new List<int>{1,2,3}},
                };
                public void Create()
                {
                    
                    var cl = from i in _cool_list
                        group i by i.pts[0]!=0;
                    
                    foreach (var i in cl)
                    { 
                        Console.WriteLine(i.Key);
                        foreach (var j in i)
                        {
                            Console.WriteLine(j.pts[0] + " " + j.id);
                        }
                    }
                }
            }
            
        }

        namespace E2
        {
            public class st
            {
                public int id { get; set; }
                public List<int> pts;
            }
            
            class Groui
            {
                private List<st> _cool_list = new List<st>
                {
                    new st {id = 0, pts = new List<int>{0,1,2,3,4}},
                    new st {id = 1, pts = new List<int>{0,1,2,3,5}},
                    new st {id = 2, pts = new List<int>{1,2,3}},
                    new st {id = 3, pts = new List<int>{1,2,3}},
                };
                
                public void Create()
                {

                    var cl = from i in _cool_list
                        group i by i.pts[0]
                        into f_add
                        where f_add.Key == 1 || f_add.Key == 0
                        select new { f = f_add.Key, w = f_add.Count() };
                    
                    foreach (var i in cl)
                    { 
                            Console.WriteLine(i.f + " " + i.w);
                    }
                    
                }
            }
            
        }
        
    }
}
public class Program
{

    static void Main(string[] args)
    {
 
    }
    private static void GroupINto()
    {
        Query.Group.E1.Group g = new Group();
        g.Create();
        Query.Group.E2.Groui g2 = new Groui();
        g2.Create();

    }
    private static void From()
    {
        Query.From.E1.From q = new From();
        q.ExecuteQuery();
        Query.From.E2.Query q1 = new Query.From.E2.Query();
        q1.Create();
    }
}