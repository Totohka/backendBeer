using DAL.Entities;
using Model.Entities;
using System.Linq.Expressions;

namespace DAL.Repositories
{
    public interface IBaseRepository<T>
            where T : BaseEntity
    {
        /// <summary>
        /// Получение сущности по уникальному идентификатору
        /// </summary>
        /// <param name="uid">Уникальный идентификатор</param>
        /// <returns>Сущность</returns>
        public Task<T> GetAsync(Guid uid);

        /// <summary>
        /// Получение сущности по выражению
        /// </summary>
        /// <param name="predicate">Лямбда функция</param>
        /// <returns>Сущность</returns>
        public Task<T> GetAsync(Expression<Func<T, bool>> predicate);

        /// <summary>
        /// Получение всех сущностей
        /// </summary>
        /// <returns>Список сущностей</returns>
        public Task<List<T>> GetAllAsync();

        /// <summary>
        /// Получение списка сущностей
        /// </summary>
        /// <param name="predicate">Лямда функция</param>
        /// <returns>Список сущностей</returns>
        public Task<List<T>> GetAllAsync(Expression<Func<T, bool>> predicate);
        
        /// <summary>
        /// Получение списка сущностей + пагинация
        /// </summary>
        /// <param name="predicate">Лямда функция</param>
        /// <returns>Список сущностей</returns>
        public Task<ResponsePagination<T>> GetPartAsync(Expression<Func<T, bool>> predicate, int skip, int take);

        /// <summary>
        /// Создание сущности
        /// </summary>
        /// <param name="item">Сущность</param>
        /// <returns></returns>
        public Task<Guid> CreateAsync(T item);

        /// <summary>
        /// Редактирование сущности
        /// </summary>
        /// <param name="item">Сущность</param>
        /// <returns></returns>
        public Task UpdateAsync(T item);

        /// <summary>
        /// Удаление сущности по уникальному идентификатору
        /// </summary>
        /// <param name="uid">Уникальный идентификатор</param>
        /// <returns></returns>
        public Task DeleteAsync(Guid uid);

        /// <summary>
        /// Удаление сущностей по условию(использовать с осторожностью)
        /// </summary>
        /// <param name="predicate">Лямда выражение</param>
        /// <returns></returns>
        public Task DeleteAsync(Expression<Func<T, bool>> predicate);
    }
}
