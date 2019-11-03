using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;
using BikeStore.core.Domain.Product_NS;
using BikeStore.core.Domain;
using BikeStore.core.Domain.Notification_NS;

namespace BikeStore.Infrastructure.EF {

  public abstract class DB_TABLE {

    public const string DBInfo = "dbinfo";
    public const string Products = "product";
    public const string ProductCategory = "productcategory";
    public const string ProductImages = "productimages";
    public const string Users = "users";
    public const string Notification = "notifications";
    public const string NotificationImage = "notificationimages";
    public const string NotificationMessage = "notificationmessages";
    public const string Orders = "orders";
    public const string OrderItems = "orderitems";

  }

  public static class ExtensionsDataBase {

    private const int DB_VERSION = (4);

    public static IWebHost UpdateDatabase(this IWebHost xIWebHost) {
      // funkcja jako rozszrzenie, aktualizująca bazę danych

      IServiceScopeFactory pIServiceScopeFactory;
      IServiceProvider xIServiceProvider;
      BikeStoreContext pBikestoreContext;
      int pDBVersion;

      pIServiceScopeFactory = (IServiceScopeFactory)xIWebHost.Services.GetService(typeof(IServiceScopeFactory));

      using (IServiceScope xIServiceScope = pIServiceScopeFactory.CreateScope()) {
        xIServiceProvider = xIServiceScope.ServiceProvider;
        pBikestoreContext = xIServiceProvider.GetRequiredService<BikeStoreContext>();

        if (!pBikestoreContext.Database.IsNpgsql()) return xIWebHost;

        TryCreateTableDBInfo(pBikestoreContext.Database);

        pDBVersion = GetDBVersion(pBikestoreContext.Database);
        if (!(pDBVersion < DB_VERSION)) return xIWebHost;

        UpdateDB(pBikestoreContext.Database);

        SetDBVersion(pBikestoreContext.Database, DB_VERSION);


      }

      return xIWebHost;

    }
    private static void SetDBVersion(DatabaseFacade xDatabase, int xDBVersion) {
      // funkcja zapisująca informację o wersji bazy w tabeli DBInfo
      // xDatabase - obiekt do bazy danych

      string pSql = $"INSERT INTO {DB_TABLE.DBInfo} (dbversion) VALUES ({xDBVersion});";

      xDatabase.TryExecuteSqlCommand(pSql);

    }
    private static void UpdateDB(DatabaseFacade xDatabase) {
      // funkcja aktualizująca bazę
      // xDatabase - obiekt do bazy danych

      foreach (string pSql in GetCollectionSql()) {
        xDatabase.TryExecuteSqlCommand(pSql);
      }

    }
    private static IEnumerable<string> GetCollectionSql() {
      // funkcja zwracająca kolekcję poleceń do aktualizacji schematu bazy danych

      foreach (string pSql in GetSqlToCreateTableProductCategory()) {
        yield return pSql;
      }

      foreach (string pSql in GetSqlToCreateTableProduct()) {
        yield return pSql;
      }

      foreach (string pSql in GetSqlToCreateTableProductImages()) {
        yield return pSql;
      }

      foreach (string pSql in GetSqlToCreateTableUsers()) {
        yield return pSql;
      }

      foreach (string pSql in GetSqlToCreateTableNotification()) {
        yield return pSql;
      }

      foreach (string pSql in GetSqlToCreateTableNotificationImages()) {
        yield return pSql;
      }
      
      foreach (string pSql in GetSqlToCreateTableNotificationMessages()) {
        yield return pSql;
      }

      foreach (string pSql in GetSqlToCreateTableOrder()) {
        yield return pSql;
      }
      
      foreach (string pSql in GetSqlToCreateTableOrderItems()) {
        yield return pSql;
      }

    }
    private static int GetDBVersion(DatabaseFacade xDatabase) {
      // funkcja zwracająca wersję bazy danych zapisaną w tabeli DBInfo
      // xDatabase - obiekt do bazy danych

      string pSql;
      int pDBVersion = 0;
      System.Data.Common.DbCommand pCommand;
      System.Data.Common.DbDataReader pDataReader;

      pSql = $@"SELECT DBVersion FROM {DB_TABLE.DBInfo} WHERE IdxDBInfo = (SELECT MAX(IdxDBInfo) FROM {DB_TABLE.DBInfo});";

      pCommand = xDatabase.GetDbConnection().CreateCommand();
      pCommand.CommandText = pSql;

      pCommand.Connection.Open();

      pDataReader = pCommand.ExecuteReader();
      if (pDataReader.HasRows) {
        pDataReader.Read();
        pDBVersion = pDataReader.GetInt32(0);
      }
      pDataReader.Close();

      pCommand.Connection.Close();

      return pDBVersion;

    }
    private static void TryCreateTableDBInfo(DatabaseFacade xDatabase) {
      // funkcja tworząca tabelę DBInfo
      // xDatabase - obiekt do bazy danych

      string pSql = $@"
        CREATE TABLE IF NOT EXISTS {DB_TABLE.DBInfo} (
          IdxDBInfo SERIAL PRIMARY KEY NOT NULL,
          DBVersion int DEFAULT 0 NOT NULL,
          DateOfUpdate timestamp DEFAULT now() 
        );";

      xDatabase.TryExecuteSqlCommand(pSql);

    }
    public static int TryExecuteSqlCommand(this DatabaseFacade xDataBase, RawSqlString xSql, params object[] xParameters) {
      // funkcja rozszerzenie wykonująca komende sql w try'u
      // xSql - polecenie sql
      // xParameters - parametry

      try {
        return xDataBase.ExecuteSqlCommand(xSql, xParameters);
      } catch (Exception pExp) {
        System.Diagnostics.Debug.WriteLine($"TryExequteSqlCommand:  {pExp.Message}");
      }

      return (-9999);

    }

    private static IEnumerable<string> GetSqlToCreateTableProduct() {

      yield return $@"
        CREATE TABLE IF NOT EXISTS {DB_TABLE.Products}(
          {nameof(Product.IdxProduct)} SERIAL PRIMARY KEY,
          {nameof(Product.Name)} TEXT,
          {nameof(Product.Description)} TEXT,
          {nameof(Product.Price)} DECIMAL DEFAULT 0.0,
          {nameof(Product.IdxCategory)} INTEGER REFERENCES {DB_TABLE.ProductCategory} ({nameof(BikeStore.core.Domain.Product_NS.ProductCategory.IdxProductCategory)}) ON DELETE SET NULL,
          {nameof(Product.CreateAt)} timestamp,
          {nameof(Product.EditAt)}   timestamp
        )";

      yield return $@"
       ALTER TABLE {DB_TABLE.Products}
              ADD COLUMN IF NOT EXISTS {nameof(BikeStore.core.Domain.Product_NS.Product.Quantity)} INTEGER DEFAULT 0";
    }

    private static IEnumerable<string> GetSqlToCreateTableProductCategory() {

      yield return $@"
          CREATE TABLE IF NOT EXISTS {DB_TABLE.ProductCategory}(
            IdxProductCategory SERIAL PRIMARY KEY,
            Name TEXT

          )";
    }

    private static IEnumerable<string> GetSqlToCreateTableProductImages() {

      yield return $@"
        CREATE TABLE IF NOT EXISTS {DB_TABLE.ProductImages}(
          {nameof(ProductImage.IdxProductImage)} SERIAL PRIMARY KEY, 
          {nameof(ProductImage.IdxProduct)}  INTEGER REFERENCES {DB_TABLE.Products} ({nameof(BikeStore.core.Domain.Product_NS.Product.IdxProduct)}) ON DELETE CASCADE,
          {nameof(ProductImage.Name)} TEXT,
          {nameof(ProductImage.Size)} INTEGER,
          {nameof(ProductImage.Content)} BYTEA
      )";
    }

    private static IEnumerable<string> GetSqlToCreateTableUsers() {

      yield return $@"
        CREATE TABlE IF NOT EXISTS {DB_TABLE.Users}(
          {nameof(User.IdxUser)} SERIAL PRIMARY KEY,
          {nameof(User.CreatedAt)} TIMESTAMP,
          {nameof(User.Email)} TEXT,
          {nameof(User.Id)} UUID,
          {nameof(User.IsEmailConfirm)} BOOLEAN NOT NULL DEFAULT FALSE,
          {nameof(User.Name)} TEXT,
          {nameof(User.Password)} TEXT,
          {nameof(User.Role)} TEXT,
          {nameof(User.Salt)} TEXT, 
          {nameof(User.Surname)} TEXT,
          {nameof(User.UpdatedAt)} TIMESTAMP 
        )";
    }

    private static IEnumerable<string> GetSqlToCreateTableNotification() {

      yield return $@"
        CREATE TABLE IF NOT EXISTS {DB_TABLE.Notification}(
          {nameof(Notification.IdxNotification)} SERIAL PRIMARY KEY,
          {nameof(Notification.IdxUser)} INTEGER REFERENCES {DB_TABLE.Users} ({nameof(User.IdxUser)}) ON DELETE CASCADE,
          {nameof(Notification.Guid)} UUID,
          {nameof(Notification.CreateAt)} TIMESTAMP,
          {nameof(Notification.UpdateAt)} TIMESTAMP,
          {nameof(Notification.NotificationStatus)} INTEGER,
          {nameof(Notification.Dscr)} TEXT,
          {nameof(Notification.UserUuId)} UUID,
          {nameof(Notification.NotificationType)} INTEGER,
          {nameof(Notification.Brand)} TEXT,
          {nameof(Notification.Model)} TEXT
        )
      ";

    }

    private static IEnumerable<string> GetSqlToCreateTableNotificationImages() {

      yield return $@"
        CREATE TABLE IF NOT EXISTS {DB_TABLE.NotificationImage}(
          {nameof(NotificationImage.IdxNotoificationImage)} SERIAL PRIMARY KEY,
          {nameof(NotificationImage.IdxNotification)} INTEGER REFERENCES {DB_TABLE.Notification} ({nameof(Notification.IdxNotification)}) ON DELETE CASCADE,
          {nameof(NotificationImage.Name)} TEXT,
          {nameof(NotificationImage.Size)} INTEGER,
          {nameof(NotificationImage.Content)} BYTEA
        )
      ";

    }

    private static IEnumerable<string> GetSqlToCreateTableNotificationMessages() {

      yield return $@"
        CREATE TABLE IF NOT EXISTS {DB_TABLE.NotificationMessage}(
          {nameof(NotificationMessage.IdxNotificationMessage)} SERIAL PRIMARY KEY,
          {nameof(NotificationMessage.IdxUser)} INTEGER REFERENCES {DB_TABLE.Users} ({nameof(User.IdxUser)}) ON DELETE CASCADE,
          {nameof(NotificationMessage.IdxNotfication)} INTEGER REFERENCES {DB_TABLE.Notification} ({nameof(Notification.IdxNotification)}) ON DELETE CASCADE,
          {nameof(NotificationMessage.Message)} TEXT,
          {nameof(NotificationMessage.CreateAT)} TIMESTAMP
        )
      ";
    }

    private static IEnumerable<string> GetSqlToCreateTableOrder() {

      yield return $@"
        CREATE TABLE IF NOT EXISTS {DB_TABLE.Orders}(
          {nameof(Order.IdxOrder)} SERIAL PRIMARY KEY,
          {nameof(Order.UuId)} UUID,
          {nameof(Order.IdxUser)} INTEGER,
          {nameof(Order.OrderStatus)} INTEGER,
          {nameof(Order.IdxDeleliveryAddress)} INTEGER NOT NULL,
          {nameof(Order.IsInvoice)} BOOLEAN DEFAULT FALSE,
          {nameof(Order.IdxInvoiceAddress)} INTEGER,
          {nameof(Order.OrderAttention)} TEXT,
          {nameof(Order.IsAcceptStoreRules)} BOOLEAN NOT NULL,
          {nameof(Order.IsAcceptElectronicInvoice)} BOOLEAN DEFAULT TRUE,
          {nameof(Order.DeleliveryMethod)} INTEGER,
          {nameof(Order.PaymentMethod)} INTEGER
      )";
    }

    private static IEnumerable<string> GetSqlToCreateTableOrderItems() {

      yield return $@"
        CREATE TABLE IF NOT EXISTS {DB_TABLE.OrderItems} (
          {nameof(OrderItem.IdxOrderItem)} SERIAL PRIMARY KEY,
          {nameof(OrderItem.IdxOrder)} INTEGER REFERENCES {DB_TABLE.Orders} ({nameof(Order.IdxOrder)}) ON DELETE CASCADE,
          {nameof(OrderItem.Name)} TEXT,
          {nameof(OrderItem.Quantiti)} INTEGER,
          {nameof(OrderItem.UnitPrice)}  DECIMAL DEFAULT 0.0,
          {nameof(OrderItem.IdxProduct)} INTEGER NOT NULL DEFAULT 0
        )
      ";
    }

  }

}
