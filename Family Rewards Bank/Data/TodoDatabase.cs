using Family_Rewards_Bank.Models;
using SQLite;
namespace Family_Rewards_Bank.Data
{
    public class TodoDatabase
    {
        private readonly SQLiteAsyncConnection _connection;
        public TodoDatabase()
        {
            string dbPath = Path.Combine(FileSystem.AppDataDirectory, "Todo.db");
            _connection = new SQLiteAsyncConnection(dbPath);
            _connection.CreateTableAsync<EventItem>().Wait();
        }
        public Task<List<EventItem>> GetItemsAsync()
        {
            return _connection.Table<EventItem>().ToListAsync();
        }

        public async Task<int> SaveItem(EventItem item)
        {
            if (item == null)
                throw new ArgumentNullException(nameof(item));

            try
            {
                // Ищем существующий объект по Id
                var existingItem = await _connection.FindAsync<EventItem>(item.Id);

                if (existingItem == null)
                {
                    // Если нет — вставляем
                    return await _connection.InsertAsync(item);
                }
                else
                {
                    // Если есть — обновляем
                    return await _connection.UpdateAsync(item);
                }
            }
            catch (Exception ex)
            {
                // Можно логировать ex
                throw new Exception("Ошибка при сохранении EventItem", ex);
            }
        }

        public async Task<int> DeleteItem(Guid? itemId)
        {
            if(itemId == null)
                throw new ArgumentNullException(nameof(itemId));
            
            try
            {
                var item = await _connection.FindAsync<EventItem>(itemId);
                if (item != null)
                {
                    return await _connection.DeleteAsync(item);
                }
                else
                {
                    await Application.Current.MainPage.DisplayAlert("Ошибка", "Элемент с таким ID не найден", "OK");
                    return 0;
                }
            }
            catch (Exception ex)
            {
                await Application.Current.MainPage.DisplayAlert("Ошибка", ex.Message, "OK");
                return 0;
            }

        }
    }

}

