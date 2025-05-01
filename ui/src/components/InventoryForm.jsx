export default function InventoryForm({ item, itemTypes, onChange, onSubmit }) {

    if (!item) return <div>Loading...</div>

    return (
        <form className="flex flex-col items-center"
        onSubmit={onSubmit}>
            <label className="mt-[50px] mb-[20px]">Enter the item information below:</label>
            <div className="w-[240px]">
                <input className="mb-[30px] mr-[10px]"
                type="checkbox" name="Active"
                checked={item.Active}
                onChange={onChange}/>
                <label htmlFor="active">Active</label>
            </div>
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
            placeholder="Model name"
            value={item.Model}
            onChange={onChange}
            />
            <div className="flex flex-col">
                <label className="mb-[10px]"
                htmlFor="notes">Notes</label>
                <textarea className="w-[360px] px-[20px] py-[12px] mb-[20px] border rounded-sm resize-none" 
                name="Notes" rows="6"
                value={item.Notes? item.Notes : ""}
                onChange={onChange}></textarea>
            </div>
            <button className="w-[125px] py-[12px] border rounded-sm bg-[#014880] text-white"
            type="submit"
            >Save</button>
        </form>
    );
}
