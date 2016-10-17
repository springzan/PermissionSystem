using Permission.domain.Entities;
using Permission.domain.IRepositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Permission.efcore.Repositories
{
    /// <summary>
    ///  用户管理仓储实现
    /// </summary>
    public class UserRepository : RepositoryBase<User>, IUserRepository
    {
        public UserRepository(PermissionDbContext dbContext) : base(dbContext)
        {
        }

        /// <summary>
        /// 检查用户是否存在
        /// </summary>
        /// <param name="userName">用户名</param>
        /// <param name="password">密码</param>
        /// <returns>存在返回用户实体，否则返回NULL</returns>
        public User CheckUser(string userName, string password)
        {
            return _dbContext.Set<User>().FirstOrDefault(u=>u.UserName==userName && u.Password==password);
        }
    }
}
