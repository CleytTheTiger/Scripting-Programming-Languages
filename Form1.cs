using MaterialSkin.Controls;
using NLua;
using System;
using System.Windows.Forms;

namespace Lab_1
{
    public partial class Form1 : MaterialForm
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void materialButton1_Click(object sender, EventArgs e)
        {
            double a = 0;
            double b = 0;
            double c = 0;
            materialMultiLineTextBox1.Text = String.Empty;
            Exception error = null;
            try
            {
                a = Double.Parse(textBoxA.Text);
                b = Double.Parse(textBoxB.Text);
                c = Double.Parse(textBoxC.Text);
            }
            catch (FormatException ex)
            {
                MessageBox.Show("Введите число!",ex.ToString());
                error = ex;
            }
            finally
            {
                if (error == null)
                {
                    a = Double.Parse(textBoxA.Text);
                    b = Double.Parse(textBoxB.Text);
                    c = Double.Parse(textBoxC.Text);
                }
            }

            if (error == null)
            {
                string line = makeEq(a, b, c);
                insertLine("--- Решение с дискриминантом: ---");
                insertLine(line);
                using (Lua state = new Lua())
                {
                    state.DoFile("D:\\Desktop\\Projects and sona\\Учебная херь\\Курс 3, семестр 1\\Сценарные языки программирования\\Лабораторная 1\\Lab_1\\script.lua");
                    // --- 1 способ решения ---
                    var getD = state["getD"] as LuaFunction;
                    var get2XwithD = state["get2XwithD"] as LuaFunction;
                    var getXWithD = state["getX"] as LuaFunction;
                    state["D"] = getD.Call(a, b, c)[0];
                    insertLine("Дискриминант --- " + state["D"].ToString());
                    if (Double.Parse(state["D"].ToString()) > 0)
                    {
                        double sqrtD = Math.Sqrt(Double.Parse(state["D"].ToString()));
                        var res = get2XwithD.Call(a, b, sqrtD);
                        state["x1"] = res[0];
                        state["x2"] = res[1];
                        insertLine("X1 = " + state["x1"].ToString());
                        insertLine("X2 = " + state["x2"].ToString());
                    }
                    else if (Double.Parse(state["D"].ToString()) == 0)
                    {
                        state["x1"] = getXWithD.Call(a, b)[0].ToString();
                        insertLine("X1 = " + state["x1"].ToString());
                    }
                    else
                    {
                        insertLine("Корней нет!");
                    }
                }
            }
        }

        private void insertLine(string str)
        {
            materialMultiLineTextBox1.Enabled = true;
            materialMultiLineTextBox1.Text += str+"\r\n";
            materialMultiLineTextBox1.Enabled = false;
        }
        private string makeEq(double a, double b, double c)
        {
            string line = String.Empty;
            line += a.ToString() + "x^2";
            if (b > 0)
            {
                line += "+" + b.ToString() + "x";
            }
            else
            {
                line += b.ToString() + "x";
            }
            if (c > 0)
            {
                line += "+" + c.ToString();
            }
            else
            {
                line += c.ToString();
            }

            return line;
        }
    }
}
