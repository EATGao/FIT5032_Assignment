using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FIT5032_MyCodeSnippet.Models.Exercise
{
    public class ExampleDictionary
    {
        public void Example()
        { 
            Dictionary<int, Student> studentDictionary = new Dictionary<int, Student>();

            Student s1 = new Student("Russel", "0423456789");
            Student s2 = new Student("Jian", "0456789123");

            studentDictionary.Add(1, s1);
            studentDictionary.Add(2, s2);

            Student result = new Student("", "");

            studentDictionary.TryGetValue(1, out result);
        }
    }
}