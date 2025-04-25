export default function InventoryForm({ item, itemTypes, onChange, onSubmit }) {

    if (!item) return <div>Loading...</div>

    return (
        <form onSubmit={onSubmit}>
            <label>Enter the information below:</label>
            <input type="text" name="SerialNumber"
                value={item.SerialNumber}
                onChange={onChange}
            />
            <select name="ItemTypeId"
                value={item.ItemTypeId}
                onChange={onChange}
            >
                <option value="">Select an option...</option>
                {itemTypes && itemTypes.map((itemType,index)=>(
                    <option 
                    key={index} 
                    value={itemType.Id}>
                        {itemType.Name}
                    </option>
                ))}
            </select>
            <input type="text" name="Model"
                value={item.Model}
                onChange={onChange}
            />
            <button type="submit">Save</button>
        </form>
    );
}
