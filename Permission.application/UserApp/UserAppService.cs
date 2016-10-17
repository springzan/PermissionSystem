using Permission.domain.Entities;
using Permission.domain.IRepositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Permission.application.UserApp
{
    /// <summary>
    /// 用户管理服务
    /// </summary>
    public class UserAppService : IUserAppService
    {
        //用户管理仓储接口
        private readonly IUserRepository _userRepository;

        /// <summary>
        /// 构造函数 实现依赖注入
        /// </summary>
        /// <param name="userRepository">仓储对象</param>
        public UserAppService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public User CheckUser(string userName, string password)
        {
            return _userRepository.CheckUser(userName,password);
        }
    }
}
