namespace Cave_Adventure.Objects.Items
{
    public abstract class Item
    {
        public ItemType Tag { get; protected init; }

        protected Item(ItemType tag)
        {
            Tag = tag;
        }
    }
}