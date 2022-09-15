using System.Net;
using AutoMapper;
using AutoMapperDemo.Models;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace AutoMapperDemo.Controllers
{
    [ApiController]
    public class TestController : ControllerBase
    {
        public TestController() { }

        [HttpPost]
        [Route("TestAutoMapper")]
        [SwaggerResponse((int)HttpStatusCode.OK, "Test OK", typeof(UIModel))]
        public IActionResult TestAutoMapper([FromBody] ORModel ormData)
        {
            // 1.映射规则配置
            var mapperConfig = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<ORModel, UIModel>()
                    .ForMember(ui => ui.Gender, opt => opt.MapFrom(orm => orm.Gender == 1 ? "男" : "女"))
                    .ForMember(ui => ui.StudentName, opt => opt.MapFrom(orm => orm.Name))
                    .ForMember(ui => ui.BirthDay, opt => opt.MapFrom(orm => orm.BirthDay.ToString("yyyy-MM-dd HH:mm:ss")));
            });
            // 2.创建映射的实例
            var mapper = mapperConfig.CreateMapper();
            // 3.数据映射
            var uiData = mapper.Map<UIModel>(ormData);
            return Ok(uiData);
        }
    }
}