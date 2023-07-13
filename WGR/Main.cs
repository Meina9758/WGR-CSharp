using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WGR
{
    public partial class Main : Form
    {
        public Main()
        {
            InitializeComponent();
            checkedListBox1.Items.Add(new Option { Name = "彩虹六号围攻", IPAddress = "203.132.26.137", Domain = "lb-rdv-siege-xplay-multiaz-prod.ubisoft.com" });
            checkedListBox1.Items.Add(new Option { Name = "彩虹六号异种", IPAddress = "203.132.18.214", Domain = "lb-rdv-pioneer-prod.ubisoft.com" });
            checkedListBox1.Items.Add(new Option { Name = "极限巅峰", IPAddress = "203.132.20.17", Domain = "lb-rdv-catalog-pub.ubisoft.com" });
            checkedListBox1.Items.Add(new Option { Name = "荣耀战魂", IPAddress = "203.132.26.98", Domain = "lb-rdv-hero-pc-multiaz-prod.ubisoft.com" });
            checkedListBox1.Items.Add(new Option { Name = "看门狗2", IPAddress = "203.132.26.146", Domain = "lb-rdv-as-prod01.ubisoft.com" });
            checkedListBox1.Items.Add(new Option { Name = "幽灵行动荒野", IPAddress = "203.132.26.64", Domain = "lb-z01-rdv-grw-prod.ubisoft.com" });
            checkedListBox1.Items.Add(new Option { Name = "孤岛惊魂5", IPAddress = "203.132.26.146", Domain = "lb-rdv-as-prod01.ubisoft.com" });
            checkedListBox1.Items.Add(new Option { Name = "看门狗", IPAddress = "216.98.55.73", Domain = "lb-rdv-wd.ubisoft.com" });
            checkedListBox1.Items.Add(new Option { Name = "幽灵行动断点", IPAddress = "203.132.20.7", Domain = "lb-maz-rdv-tgt-prod.ubisoft.com" });
        }


        private List<string> ipList = new List<string>();
        private List<string> domainList = new List<string>();


        private class Option
        {
            public string Name { get; set; }
            public string IPAddress { get; set; }
            public string Domain { get; set; }

            public override string ToString()
            {
                return Name;
            }
        }
        // 当用户选中或取消选中复选框时触发该事件
        private void checkedListBox1_ItemCheck(object sender, ItemCheckEventArgs e)
        {
            Option selectedOption = (Option)checkedListBox1.Items[e.Index];

            if (e.NewValue == CheckState.Checked)
            {
                // 将选中的选项的IP地址和域名添加到各自的List集合中
                ipList.Add(selectedOption.IPAddress);
                domainList.Add(selectedOption.IPAddress + " " + selectedOption.Domain);
            }
            else
            {
                // 如果用户取消选中，从各自的List集合中移除对应的IP地址和域名
                ipList.Remove(selectedOption.IPAddress);
                domainList.Remove(selectedOption.IPAddress + " " + selectedOption.Domain);
            }
        }

        public string currentDirectory = Environment.CurrentDirectory;
        private void connection_Click(object sender, EventArgs e)
        {
            
                if (checkedListBox1.CheckedItems.Count > 0)
                {
                    //写入hosts
                    try
                    {
                        string hostsFilePath = Environment.SystemDirectory + @"\drivers\etc\hosts";
                        string entries = string.Join(" #WGR\r\n", domainList) + " #WGR" + "\r\n";
                        File.AppendAllText(hostsFilePath, entries);
                    }
                    catch
                    {
                        MessageBox.Show("写入Host错误");
                    }


                    string wgcf = currentDirectory + "/wgcf-profile.conf";//设置默认wgcf配置目录
                    string acc = currentDirectory + "/wgcf-account.toml";//设置默认wgcf账号目录
                    string wgconf = currentDirectory + "/conf/wg.conf";//设置存放目录
                    if (File.Exists(wgconf))//如果有配置文件
                    {
                        try
                        {
                            // 读取文本文件的所有行
                            string[] lines = File.ReadAllLines(wgconf);
                            string AllowedIPs = string.Join(",", ipList);//转换ipList成String

                            // 替换行
                            int lineIndex = 8;
                            List<string> WGRC = new List<string>(File.ReadAllLines(wgconf));

                            // 替换指定行
                            WGRC[lineIndex] = "AllowedIPs =" + AllowedIPs;

                            // 将修改后的内容写回文件
                            File.WriteAllLines(wgconf, WGRC);
                        }
                        catch (IOException ex)
                        {
                            MessageBox.Show("发生错误: " + ex.Message);
                        }

                        //连接wireguard
                        Process WGprocess = new Process();

                        // 设置进程启动信息
                        ProcessStartInfo WGstartInfo = new ProcessStartInfo
                        {
                            FileName = "cmd.exe", // 指定要执行的命令解释器
                            RedirectStandardInput = true, // 重定向输入流
                            RedirectStandardOutput = true, // 重定向输出流
                            CreateNoWindow = true, // 不创建新窗口
                            UseShellExecute = false // 不使用系统外壳程序启动进程
                        };

                        // 启动进程
                        WGprocess.StartInfo = WGstartInfo;
                        WGprocess.Start();

                        // 向控制台输入命令
                        //MessageBox.Show(currentDirectory + "/bin/wireguard.exe /installtunnelservice " + currentDirectory + "/conf/wg.conf");
                        WGprocess.StandardInput.WriteLine(currentDirectory + "/bin/wireguard.exe /installtunnelservice " + currentDirectory + "/conf/wg.conf"); // 连接wiregaurd配置
                        WGprocess.WaitForExit();
                    }


                    else//如果没有配置文件
                    {
                        //++++生成wgcf配置
                        Process process = new Process();

                        // 设置进程启动信息
                        ProcessStartInfo startInfo = new ProcessStartInfo
                        {
                            FileName = "cmd.exe", // 指定要执行的命令解释器
                            RedirectStandardInput = true, // 重定向输入流
                            RedirectStandardOutput = true, // 重定向输出流
                            CreateNoWindow = true, // 不创建新窗口
                            UseShellExecute = false // 不使用系统外壳程序启动进程
                        };

                        // 启动进程
                        process.StartInfo = startInfo;
                        process.Start();

                        // 向控制台输入命令
                        process.StandardInput.WriteLine(currentDirectory + "/bin/wgcf.exe register --accept-tos"); // register配置,确保cloudflare服务通畅
                        process.StandardInput.WriteLine(currentDirectory + "/bin/wgcf.exe generate");//generate配置文件
                        process.StandardInput.WriteLine("exit");//退出
                        process.WaitForExit();
                        //++++修改endpoint和iplist
                        try
                        {
                            // 读取文本文件的所有行
                            string[] lines = File.ReadAllLines(wgcf);
                            string AllowedIPs = string.Join(",", ipList);//转换ipList成String

                            // 替换字段
                            for (int i = 0; i < lines.Length; i++)
                            {
                                // 使用 Replace 方法替换指定的字段
                                lines[i] = lines[i].Replace("engage.cloudflareclient.com", "162.159.193.1");
                                lines[i] = lines[i].Replace("0.0.0.0/0", AllowedIPs);
                            }

                            // 将修改后的内容写回文件
                            File.WriteAllLines(wgconf, lines);
                            File.Delete(wgcf);//删除生成的默认配置
                            File.Delete(acc);//删除生成的账号配置
                        }
                        catch (IOException ex)
                        {
                            MessageBox.Show("发生错误: " + ex.Message);
                        }
                        //连接wireguard
                        Process WGprocess = new Process();

                        // 设置进程启动信息
                        ProcessStartInfo WGstartInfo = new ProcessStartInfo
                        {
                            FileName = "cmd.exe", // 指定要执行的命令解释器
                            RedirectStandardInput = true, // 重定向输入流
                            RedirectStandardOutput = true, // 重定向输出流
                            CreateNoWindow = true, // 不创建新窗口
                            UseShellExecute = false // 不使用系统外壳程序启动进程
                        };

                        // 启动进程
                        WGprocess.StartInfo = WGstartInfo;
                        WGprocess.Start();

                        // 向控制台输入命令
                        //MessageBox.Show(currentDirectory + "/bin/wireguard.exe /installtunnelservice " + currentDirectory + "/conf/wg.conf");
                        WGprocess.StandardInput.WriteLine(currentDirectory + "/bin/wireguard.exe /installtunnelservice " + currentDirectory + "/conf/wg.conf"); // 连接wiregaurd配置
                        WGprocess.StandardInput.WriteLine("exit");//退出
                    }
                }
                else
                {
                    MessageBox.Show("至少选择一项");
                }

        }

        private void Main_Load(object sender, EventArgs e)
        {
            cfwarp.Checked = true;
        }

        private void disconnect_Click(object sender, EventArgs e)
        {
                //断开wireguard
                Process DWGprocess = new Process();

                // 设置进程启动信息
                ProcessStartInfo DWGstartInfo = new ProcessStartInfo
                {
                    FileName = "cmd.exe", // 指定要执行的命令解释器
                    RedirectStandardInput = true, // 重定向输入流
                    RedirectStandardOutput = true, // 重定向输出流
                    CreateNoWindow = true, // 不创建新窗口
                    UseShellExecute = false // 不使用系统外壳程序启动进程
                };

                // 启动进程
                DWGprocess.StartInfo = DWGstartInfo;
                DWGprocess.Start();

                // 向控制台输入命令
                DWGprocess.StandardInput.WriteLine(currentDirectory + "/bin/wireguard.exe /uninstalltunnelservice wg"); // 断开默认wg接口配置
                DWGprocess.StandardInput.WriteLine("exit");//退出
                DWGprocess.WaitForExit();


                //删除hosts
                try
                {
                    string hostsFilePath = Environment.SystemDirectory + @"\drivers\etc\hosts";
                    string searchString = "#WGR";

                    // 读取文件的所有行
                    List<string> lines = new List<string>(File.ReadAllLines(hostsFilePath));

                    // 遍历行并删除包含特定字符串的行
                    for (int i = lines.Count - 1; i >= 0; i--)
                    {
                        if (lines[i].Contains(searchString))
                        {
                            lines.RemoveAt(i);
                        }
                    }

                    // 将修改后的行写回文件
                    File.WriteAllLines(hostsFilePath, lines);
                }
                catch
                {
                    MessageBox.Show("删除Host错误,请手动删除");
                }
            
        }
    }
}
