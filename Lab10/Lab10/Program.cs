DateSelector dateSelector = new DateSelector();
DeliveryDateField deliveryDateField = new DeliveryDateField();
AnotherRecipientCheckBox anotherRecipientCheckBox = new AnotherRecipientCheckBox();
NameAndPhoneField nameAndPhoneField = new NameAndPhoneField();
SelfCheckoutCheckBox selfCheckoutCheckBox = new SelfCheckoutCheckBox();
PageRenderer pageRenderer = new PageRenderer();
Mediator mediator = new OrderFormMediator(dateSelector, deliveryDateField, anotherRecipientCheckBox, nameAndPhoneField, selfCheckoutCheckBox, pageRenderer);


pageRenderer.RenderPage(dateSelector.visible, deliveryDateField.visible, anotherRecipientCheckBox.visible, anotherRecipientCheckBox.state, nameAndPhoneField.visible, selfCheckoutCheckBox.state);
dateSelector.SelectDate();
anotherRecipientCheckBox.SetState(true);
anotherRecipientCheckBox.SetState(false);
selfCheckoutCheckBox.SetState(true);
selfCheckoutCheckBox.SetState(false);


public abstract class Mediator()
{
    public abstract void Notify(Component sender, string eventType);
}
public class OrderFormMediator : Mediator
{
    private DateSelector dateSelector;
    private DeliveryDateField deliveryDateField;
    private AnotherRecipientCheckBox anotherRecipientCheckBox;
    private NameAndPhoneField nameAndPhoneField;
    private SelfCheckoutCheckBox selfCheckoutCheckBox;
    private PageRenderer pageRenderer;
    public OrderFormMediator(DateSelector dateSelector, DeliveryDateField deliveryDateField, AnotherRecipientCheckBox anotherRecipientCheckBox, NameAndPhoneField nameAndPhoneField, SelfCheckoutCheckBox selfCheckoutCheckBox, PageRenderer pageRenderer)
    {
        this.dateSelector = dateSelector;
        this.deliveryDateField = deliveryDateField;
        this.anotherRecipientCheckBox = anotherRecipientCheckBox;
        this.nameAndPhoneField = nameAndPhoneField;
        this.selfCheckoutCheckBox = selfCheckoutCheckBox;
        this.pageRenderer = pageRenderer;

        this.dateSelector.SetMediator(this);
        this.deliveryDateField.SetMediator(this);
        this.anotherRecipientCheckBox.SetMediator(this);
        this.nameAndPhoneField.SetMediator(this);
        this.selfCheckoutCheckBox.SetMediator(this);
        this.pageRenderer.SetMediator(this);
    }
    public override void Notify(Component sender, string eventType)
    {
        if (sender == dateSelector && eventType == "date selected")
        {
            deliveryDateField.visible = true;
            Console.WriteLine("\n------------------------\nDate selected\n------------------------");
        }
        else if (sender == anotherRecipientCheckBox && eventType == "AnotherRecipientCheckBox False")
        {
            nameAndPhoneField.visible = false;
            Console.WriteLine("\n------------------------\nAnotherRecipientCheckBox False\n------------------------");
        }
        else if (sender == anotherRecipientCheckBox && eventType == "AnotherRecipientCheckBox True")
        {
            nameAndPhoneField.visible = true;
            Console.WriteLine("\n------------------------\nAnotherRecipientCheckBox True\n------------------------");
        }
        else if (sender == selfCheckoutCheckBox && eventType == "SelfCheckoutCheckBox True")
        {
            dateSelector.visible = false;
            deliveryDateField.visible = false;
            anotherRecipientCheckBox.visible = false;
            nameAndPhoneField.visible = false;
            Console.WriteLine("\n------------------------\nSelfCheckoutCheckBox True\n------------------------");
        }
        else if (sender == selfCheckoutCheckBox && eventType == "SelfCheckoutCheckBox False")
        {
            dateSelector.visible = true;
            deliveryDateField.visible = true;
            anotherRecipientCheckBox.visible = true;
            nameAndPhoneField.visible = true;
            Console.WriteLine("\n------------------------\nSelfCheckoutCheckBox False\n------------------------");
        }
        pageRenderer.RenderPage(dateSelector.visible, deliveryDateField.visible, anotherRecipientCheckBox.visible, anotherRecipientCheckBox.state, nameAndPhoneField.visible, selfCheckoutCheckBox.state);
    }
}
public abstract class Component
{
    protected Mediator mediator;

    public void SetMediator(Mediator mediator)
    {
        this.mediator = mediator;
    }
}
public class PageRenderer : Component
{

    public void RenderPage(bool isDateSelectorVisible, bool isDeliveryDateVisible, bool isAnotherRecipientCheckBoxVisible, bool anotherRecipientCheckBoxState, bool isNameAndPhoneFieldVisible, bool selfCheckoutCheckBoxState)
    {
        Console.WriteLine("\n----------------------------------\nDelivery form:");
        if (selfCheckoutCheckBoxState) { Console.WriteLine("\tSelf Checkout:\t\tyes"); }
        else
        {
            Console.WriteLine("\tSelf Checkout:\t\tno");
            if (isDeliveryDateVisible) { Console.WriteLine("\tDate selector:\t\t[Monday]"); }
            else { Console.WriteLine("\tDate selector:\t\t[select date]"); }
            if (anotherRecipientCheckBoxState)
            {
                Console.WriteLine("\tAnother recipient:\tyes");
                Console.WriteLine("\n\tName:\n\tPhone:");
            }
            else { Console.WriteLine("\tAnother recipient:\tno"); }
        }
    }
}
public class DateSelector : Component
{
    public bool visible = true;
    public void SelectDate()
    {
        mediator.Notify(this, "date selected");
    }
}
public class DeliveryDateField : Component
{
    public bool visible = false;
}

public class AnotherRecipientCheckBox : Component
{
    public bool visible = true;
    public bool state = false;
    public void SetState(bool state)
    {
        this.state = state;
        mediator.Notify(this, $"AnotherRecipientCheckBox {state}");
    }
}

public class NameAndPhoneField : Component
{
    public bool visible = false;
}

public class SelfCheckoutCheckBox : Component
{
    public bool visible = true;
    public bool state = false;
    public void SetState(bool state)
    {
        this.state = state;
        mediator.Notify(this, $"SelfCheckoutCheckBox {state}");
    }
}