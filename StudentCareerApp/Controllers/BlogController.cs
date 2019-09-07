using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SCA.Entity.DTO;
using SCA.Services.Interface;

namespace SCA.UI.Controllers
{
    public class BlogController : Controller
    {
        #region INTERFACES & CONSTRUCTOR
        private readonly IB2CManagerUI b2cManager;
        public BlogController(IB2CManagerUI b2CManager)
        {
            this.b2cManager = b2CManager;
        }
        #endregion


        [Route("makale/{SeoUrl}"), HttpGet]
        public async Task<IActionResult> Index(string SeoUrl)
        {
            var res = await b2cManager.GetContentDetailForDetailPage(SeoUrl);
            var fakeData = FakeContentDetail();
            return View(res);
        }

        public ContentDetailForDetailPageDTO FakeContentDetail()
        {
            return new ContentDetailForDetailPageDTO
            {
                Category = "Teknoloji",
                ContentDescription = @"<p>
                                <span class='tie-dropcap'>A</span> farmers in the US’s South—faced with continued failure in their efforts to run on successful farms their
                                launched a lawsuit claiming that “white racism” is to the blame for their inability to produce crop yields.
                            </p>
                            <p>
                                Black farmers in the US’s South—
                                <span>faced with continued failure their efforts to run successful farms</span> their launched a lawsuit claiming that
                                “white racism” is to blame for their inability to the produce crop yields and on equivalent to that switched seeds.
                            </p>
                            <blockquote>
                                Black farmers in the US’s South faced with continued failure in their efforts to run the successful farms launched a lawsuit
                                <cite>Steve Jobs</cite>
                            </blockquote>
                            <h3>
                                For the first time the Swiss State Secretart for Economic Affair (SECO) has indicated that Uber taxi drivers should
                            </h3>
                            <p>
                                Black farmers in the US’s South—
                                <span>faced with continued failure their efforts to run successful farms</span> their launched a lawsuit claiming that
                                “white racism” is to blame for their inability to the produce crop yields and on equivalent to that switched seeds.
                            </p>


                            <p>
                                Black farmers in the US’s South—
                                <span>faced with continued failure their efforts to run successful farms</span> their launched a lawsuit claiming that
                                “white racism” is to blame for their inability to the produce crop yields and on equivalent to that switched seeds.
                            </p>
                            <div class='post-video'>
                                <img class='img-fluid' src='/AdminFiles/CMS/Content/Images/3.jpg' alt=''>
                                <div class='post-video-content'>
                                    <a href = 'https://www.youtube.com/watch?v=_0UO1NcheAE' class='ts-play-btn'>
                                        <i class='fa fa-play' aria-hidden='true'></i>
                                    </a>
                                    <h3>
                                        <a href = '' > For the first time the Swiss State Secretart for Economic indicated that Uber taxi</a>
                                    </h3>
                                </div>
                            </div>
                            <h3>
                                For the first time the Swiss State Secretart for Economic Affair(SECO) has indicated that Uber taxi drivers should be classed
                                as employees
                            </h3>
                            <p>
                                Black farmers in the US’s South—faced with continued failure in their efforts to run successful farms their launched a lawsuit
                                claiming that “white racism” is to blame for their inability to produce crop yields and on equivalent to that switched
                                seeds in order to sell black farmers a subpar product at the Mid-South Farm & Gin Show in March 2017. Despite above
                                average rainfall, the black farmers saw limited soybean yield from the Stine seeds during the 2017 harvest.
                            </p>
                            <p>
                                <a href=''>
                                    <img class='img-fluid' src='/images/banner/banner2.jpg' alt=''>
                                </a>
                            </p>
                            <h3>
                                For the first time the Swiss State Secretart for Economic Affair
                            </h3>
                            <ul>
                                <li>
                                    But there was also no shortage of news for the worlds of film.
                                </li>
                                <li>
                                    Prasad made these comments on behalf of the Treasury benches.
                                </li>
                                <li>
                                    He gave details of the answers provided by Defence.
                                </li>
                                <li>
                                    The government has been forthcoming in declaring the aircraft.
                                </li>
                            </ul>
                            <p>
                                Black farmers in the US’s South—faced with continued failure in their efforts to run successful farms their launched a lawsuit
                                claiming that “white racism” is to blame for their inability
                            </p>",
                Header = "SpaceX, Çin ile yarışıyor! Uzaya gönderilen roket sayısında inanılmaz rekabet!",
                ImagePath = "/AdminFiles/CMS/Content/Images/3.jpg",
                PublishDate = new DateTime(2019, 07, 11),
                ReadCount = 126,
                Tags = "tag1, tag2, tag3",
                Writer = "Göksu Deniz",
                MostPopularItems = new List<ContentForHomePageDTO>
                {
                    new ContentForHomePageDTO
                    {
                        Category = "Teknoloji",
                        Header = "SpaceX, Çin ile yarışıyor! Uzaya gönderilen roket sayısında inanılmaz rekabet!",
                        ImagePath = "/AdminFiles/CMS/Content/Images/3.jpg",
                        IsHeadLine = true,
                        IsManset = true,
                        PublishDate = new DateTime(2019, 7, 11),
                        ReadCount = 122,
                        SeoUrl = "spacex-cin-ile-yarisiyor-uzaya-gonderilen-roket-sayisinda-inanilmaz-rekabet",
                        Writer = "Göksu Deniz"
                    },
                    new ContentForHomePageDTO
                    {
                        Category = "Eğitim",
                        Header = "UIPath, RPA alanında Türkiye'de ücretsiz serfikasyon programına başladı.",
                        ImagePath = "/AdminFiles/CMS/Content/Images/3.jpg",
                        IsHeadLine = true,
                        IsManset = true,
                        PublishDate = new DateTime(2019, 7, 11),
                        ReadCount = 74,
                        SeoUrl = "uipath-rpa-alaninda-turkiyede-ucretsiz-sertifikasyon-programina-basladi",
                        Writer = "Göksu Deniz"
                    },
                    new ContentForHomePageDTO
                    {
                        Category = "Sağlık",
                        Header = "Kansere yakın zamanda tedavi bulunabilir. İsrailli bilim adamları oral yolla alınan ilaç geliştirdi.",
                        ImagePath = "/AdminFiles/CMS/Content/Images/3.jpg",
                        IsHeadLine = true,
                        IsManset = true,
                        PublishDate = new DateTime(2019, 7, 11),
                        ReadCount = 122,
                        SeoUrl = "kansere-yakin-zamanda-tedavi-bulunabilir-israilli-bilim-adamlari-oral-yolla-alinan-ilac-gelistirdi",
                        Writer = "Göksu Deniz"
                    },
                    new ContentForHomePageDTO
                    {
                        Category = "Sağlık",
                        Header = "Bu ülkelere gitme planınız varsa tekrar düşünmenizi gerektirecek bazı durumlar",
                        ImagePath = "/AdminFiles/CMS/Content/Images/3.jpg",
                        IsHeadLine = true,
                        IsManset = true,
                        PublishDate = new DateTime(2019, 7, 11),
                        ReadCount = 196,
                        SeoUrl = "bu-ulkelere-gitme-planiniz-varsa-tekrar-dusunmenizi-gerektirecek-bazi-durumlar",
                        Writer = "Göksu Deniz"
                    }
                }
            };
        }
    }
}

