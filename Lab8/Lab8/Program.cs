﻿Console.WriteLine("\n\nUpdating product--------------------------------");
EntityUpdater productProcessor = new ProductUpdater();
productProcessor.UpdateEntity();
Console.WriteLine("\n\nUpdating user-----------------------------------");
EntityUpdater userProcessor = new UserUpdater();
userProcessor.UpdateEntity();
Console.WriteLine("\n\nUpdating order----------------------------------");
EntityUpdater orderProcessor = new OrderUpdater();
orderProcessor.UpdateEntity();
public abstract class EntityUpdater
{
    public void UpdateEntity()
    {
        object entity = GetEntity();
        if (ValidateData(entity))
        {
            SaveData(entity);
            SendResponse(200, "Success", GetMessageSuccessHook());
        }
        else
        {
            ValidationFailure(entity);
            SendResponse(400, "Failure", GetMessageFailedHook());
        }
        AfterUpdateHook();
    }
    public abstract object GetEntity();
    protected abstract bool ValidateData(object entity);
    protected abstract void SaveData(object entity);
    protected abstract void ValidationFailure(object entity);
    private void SendResponse(int code, string status, string message) { Console.WriteLine($"{code}, {status}, {message}"); }
    //hooks
    protected virtual string GetMessageSuccessHook() { return null; }
    protected virtual string GetMessageFailedHook() { return null; }
    protected virtual void AfterUpdateHook() {  }
}
public class Product
{
}
public class User
{
}
public class Order
{
}
public class ProductUpdater : EntityUpdater
{
    public override object GetEntity() { return new Product(); }
    protected override bool ValidateData(object entity) { return true; }
    protected override void SaveData(object entity) { Console.WriteLine("Saving product"); }
    protected override void ValidationFailure(object entity) { Console.WriteLine("Sending notification to messenger"); }
    protected override string GetMessageFailedHook() {return "Product validation failed"; }
    protected override void AfterUpdateHook()
    {
        Console.WriteLine("Product update finished");
    }
}
public class UserUpdater : EntityUpdater
{
    public override object GetEntity() { return new User(); }
    protected override bool ValidateData(object entity) { return true; }
    protected override void SaveData(object entity) { Console.WriteLine("Saving user data forbidding email update"); }
    protected override void ValidationFailure(object entity) { Console.WriteLine("Handling user validation failure"); }
    protected override string GetMessageFailedHook() { return "User validation failed"; }
}
public class OrderUpdater : EntityUpdater
{
    public override object GetEntity() { return new Order(); }
    protected override bool ValidateData(object entity) { return true; }
    protected override void SaveData(object entity) { Console.WriteLine("Saving Order data"); }
    protected override void ValidationFailure(object entity) { Console.WriteLine("Handling order data validation failure"); }
    protected override string GetMessageSuccessHook() { return "\"order\": {\n    \"id\": 1,\n    \"productId\": 2,\n    \"status\": \"Lost in the sea\"\n  }"; }
    protected override string GetMessageFailedHook() { return "Order validation failed"; }
}