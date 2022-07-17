using AutoMapper;
using AutoMapperDemo.Models;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using System.Net;

namespace AutoMapperDemo.Controllers
{
    [ApiController]
    public class TestController : ControllerBase
    {
        public TestController() {}

        [HttpPost]
        [Route("TestAutoMapper")]
        [SwaggerResponse((int)HttpStatusCode.OK, "Response Model", typeof(UIModel))]
        public IActionResult TestAutoMapper([FromBody] ORModel ormData)
        {
            // 1.ӳ���������
            var mapperConfig = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<ORModel, UIModel>()
                    .ForMember(ui => ui.Gender, opt => opt.MapFrom(orm => orm.Gender == 1 ? "��" : "Ů"))
                    .ForMember(ui => ui.StudentName, opt => opt.MapFrom(orm => orm.Name))
                    .ForMember(ui => ui.BirthDay, opt => opt.MapFrom(orm => orm.BirthDay.ToString("yyyy-MM-dd HH:mm:ss"))); 
            });
            // 2.����ӳ���ʵ��
            var mapper = mapperConfig.CreateMapper();
            // 3.����ӳ��
            var uiData = mapper.Map<UIModel>(ormData);
            return Ok(uiData);
        }
    }
}