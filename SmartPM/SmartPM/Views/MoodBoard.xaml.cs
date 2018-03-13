using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using SmartPM.Models.Timesheet;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using SmartPM.Models;
using SmartPM.Services;

namespace SmartPM.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MoodBoard : ContentPage
    {
        public ObservableCollection<NewsModels> News { get; set; }
        NewsModels nw = new NewsModels();
        public string userID { get; set; }
        public MoodBoard(string ID)
        {
            userID = ID;
            InitializeComponent();
            RenderAPI();

            /* News = new ObservableCollection<News>()
             {
                 {new News(){Title="นัดประชุมกับลูกค้า MCGeen", Date= DateTime.Now.AddDays(1), Detail="เพรียวบางฮอตดอกสไตล์บุญคุณซูเปอร์ สโตร์อิกัวนาไวอากร้า ไนท์ยังไงแฟ้บ ฟลุก ซูเอี๋ยซูชิ เช็งเม้ง จูเนียร์แจ็กพ็อตผู้นำสป็อต ตัวเองแพกเกจแพ็คแฟกซ์บู๊ นอร์ทหงวนธรรมาภิบาลเตี๊ยม รอยัลตี้โอเคยนตรกรรมดีพาร์ตเมนต์บาลานซ์ รอยัลตี้ตุ๋ยบราบ๊อบ วิดีโอแบล็กไคลแม็กซ์ ดีพาร์ทเมนท์﻿กรรมาชนเซ็กซ์ดอกเตอร์เซ็นเตอร์ โดมิโนโบว์ลิ่ง สหรัฐ ซิตี้แรงดูดไฮเปอร์อพาร์ทเมนต์"}},
                 {new News(){Title="แก้ไขหน้า UI Login", Date= DateTime.Now.AddDays(-5), Detail="คอรัปชันใช้งานโอเปอเรเตอร์ รายชื่อวีไอพีเนิร์สเซอรี สเตชันราเมน สตูดิโอ แอ๊บแบ๊ว นายพรานละตินฮิสี่แยก จึ๊กอีแต๋นมายาคติออร์แกนิกมอยส์เจอไรเซอร์ อาร์ติสต์ ฮอตดอกฟลอร์มั้ยเลสเบี้ยนออร์แกนิค ฮัลโลวีนเซ็กซ์ ฟลุคโดมิโนสปิริตทาวน์ดาวน์ คอรัปชั่นเยอร์บีร่าโฟนเกจิเช็งเม้ง โชห่วย ป๋านพมาศ คอลัมนิสต์เอ็กซ์โป ตุ๋ยคาร์โก้ศากยบุตรโดมิโนจูน"} },
                 {new News(){Title="แกไขหน้า UI Dashboard", Date= DateTime.Now.AddDays(-7), Detail="ฟลอร์เวสต์พาเหรดแอดมิชชั่นแฟรนไชส์ ไดเอ็ตอุปสงค์ปิกอัพ เดอะบุ๋นพุดดิ้ง เมคอัพโคโยตีฮาโลวีนเสกสรรค์ความหมาย ริกเตอร์ลิสต์ รันเวย์เซลส์ราสเบอร์รีพรีเมียม ฉลุยถูกต้องกรีนสปายซ้อ ไลน์แจ็กพอต เจ๊วโรกาสเซอร์วิสบู๊ ซิมโฟนี เวิร์กช็อปลิมิต ลาเต้ซิ้ม อุปทาน คอนโดมิเนียม เรซิ่นฟินิกซ์ดราม่าสเปค ราชานุญาตโซน "} },
                 {new News(){Title="นัดประชุม 11 โมง", Date= DateTime.Now.AddDays(-30), Detail="﻿กรรมาชนเนิร์สเซอรีเชอร์รี่โปรโมชั่น ราสเบอร์รีปิกอัพฟรังก์ เซลส์ พฤหัสว่ะ โซลาร์ตุ๋ย โปรโมชั่นโพสต์ แจ็กเก็ตฟลุต โอยัวะเป่ายิงฉุบเช็กแคปความหมาย ม็อบซากุระเอาท์ดอร์ แคมป์ มายาคตินพมาศพรีเมียม มิวสิคไอติมระโงก คณาญาติปิโตรเคมีรวมมิตรคาเฟ่ครัวซอง จึ๊ก วิลเลจ คอมเพล็กซ์แบล็ก"} },
                 {new News(){Title="พักผ่อนกายสบายใจ", Date= DateTime.Now.AddDays(-60), Detail="แซ็กลิมิตทริปเฟิร์ม สโตร์แชมป์วิลล์ มอคค่ากรีนฟาสต์ฟู้ด ตาปรือแบรนด์วานิลลา อยุติธรรม ไวกิ้งโหลยโท่ยเซอร์ พล็อตพุดดิ้งเมจิกออร์เดอร์อวอร์ด วาริชศาสตร์มาร์ชบุญคุณ พ่อค้าฮวงจุ้ยสตรอว์เบอร์รีคาแร็คเตอร์ ช็อตแลนด์แผดเผา ซาดิสม์อิกัวนาทำงานคันยิ ราชานุญาตบอดี้ ม้าหินอ่อนแฟลชนิรันดร์แอปเปิลจิ๊กซอว์ ไอติมสวีทยาวี กรอบรูปคาแรคเตอร์สึนามิเยอร์บีราคาแร็คเตอร์ โหลน "} },
                 {new News(){Title="ขึ้นเงินเดือน +30000k Baht", Date= DateTime.Now.AddDays(-90), Detail="มอคค่าสันทนาการออสซี่โนติส รีไทร์ฟีเวอร์ออทิสติกวาทกรรม สังโฆไคลแมกซ์ห่วยท็อปบู๊ท แอคทีฟ จิ๊กซอว์ควิกปิโตรเคมีโมเดลสตรอเบอรี พาสต้าเซี้ยวอัลบัมซูชิ ไฮไลต์รีสอร์ตซัพพลายบร็อคโคลี แบล็ค คาปูชิโนตนเองซ้อเรซิน โอเค พรีเมียร์ซาร์แบรนด์ควีน ฮากกาว่ะพาสเจอร์ไรส์ เมี่ยงคำซิงไกด์ เซอร์ไพรส์คำสาปโรแมนติคเธค ฟรุตแรงผลักพาสตาง่าวโพสต์ ป๋า "} },
             };
             NewsList.ItemsSource = News;*/
        }

        protected async void toolBarCreate(object sender, EventArgs e)
        {
            
            if (userID == "100017" || userID == "50")
            {
                var nav = new NavigationPage(new TopicMoodBoard(userID)) { BarBackgroundColor = Color.FromHex("#354b60"), BarTextColor = Color.White };
                await App.Current.MainPage.Navigation.PushModalAsync(nav);
            }

            else
            {

                DisplayAlert("Alert", "คุณไม่สามารถสร้างโพสได้", "OK");
            }
        }

        public async void RenderAPI()
        {
            var jsonResult = await MoodBoardService.getTopic();
            News = JsonConvert.DeserializeObject<ObservableCollection<NewsModels>>(jsonResult);
            NewsList.ItemsSource = News;
            this.IsBusy = false;

        }


        private async void newslist_ItemTapped(object sender, ItemTappedEventArgs e)
        {

            var newsItems = e.Item as NewsModels;
            nw.subject = newsItems.subject;
            nw.note = newsItems.note;
            nw.name = newsItems.name;
            nw.time = newsItems.time;
            nw.bnumber = newsItems.bnumber;





            var page = new MoodBoardDetailScreen(userID, nw);
            //App.Current.MainPage = new NavigationPage(page);
            await Navigation.PushAsync(page);
        }


        protected void Refesh(object sender, EventArgs e)
        {
            RenderAPI();
            NewsList.EndRefresh();
        }


      


    }
}