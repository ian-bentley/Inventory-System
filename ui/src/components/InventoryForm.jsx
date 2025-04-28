export default function InventoryForm({ item, itemTypes, onChange, onSubmit }) {

    if (!item) return <div>Loading...</div>

    return (
        <form className="flex flex-col items-center"
        onSubmit={onSubmit}>
            <label className="mt-[50px] mb-[20px]">Enter the item information below:</label>
            <input className="w-[240px] px-[20px] py-[12px] mb-[20px] border rounded-sm"
            type="text" name="SerialNumber"
            placeholder="Serial number"
            value={item.SerialNumber}
            onChange={onChange}
            />
            <select className="w-[240px] px-[20px] py-[12px] mb-[20px] border rounded-sm"
            name="ItemTypeId"
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
            <input className="w-[240px] px-[20px] py-[12px] mb-[20px] border rounded-sm"
            type="text" name="Model"
            value={item.Model}
            onChange={onChange}
            />
            <button className="w-[125px] py-[12px] border rounded-sm bg-[#014880] text-white"
            type="submit"
            >Save</button>
        </form>
    );
}
