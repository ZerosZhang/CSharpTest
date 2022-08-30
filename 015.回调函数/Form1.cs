using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace _015.回调函数
{
    public delegate void DisplayStudent(Student stduent);

    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();

            StudentTable _table = new StudentTable();
            _table.Display(DisplayCallBack);
        }

        void DisplayCallBack(Student student)
        {
            if (student.Sex == "女")
            {
                listBox1.Items.Add($"学号:{student.Number}\t姓名:{student.Name}\r\n");
            }
        }
    }

    class StudentTable
    {
        Student[] students;

        public StudentTable()
        {
            students = new Student[8];
            students[0] = new Student() { Number = 20120826, Name = "小红", Sex = "男" };
            students[1] = new Student() { Number = 20120826, Name = "妹妹", Sex = "男" };
            students[2] = new Student() { Number = 20120826, Name = "马里奥", Sex = "女" };
            students[3] = new Student() { Number = 20120826, Name = "路易", Sex = "女" };
            students[4] = new Student() { Number = 20120826, Name = "奇诺比奥", Sex = "男" };
            students[5] = new Student() { Number = 20120826, Name = "小蓝", Sex = "女" };
        }

        public void Display(DisplayStudent display_callback)
        {
            foreach (Student student in students)
            {
                display_callback(student);
            }
        }
    }

    public struct Student
    {
        public int Number;
        public string Name;
        public string Sex;
    }
}
