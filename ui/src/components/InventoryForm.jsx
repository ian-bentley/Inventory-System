export default function InventoryForm() {
    return(
        <>
            <form>
                <label>Enter the information below:</label>
                <input type="text" id="serial-number" name="serialNumber"/>
                <select id="type" name="type">
                    <option id="no-selection" value=''>Select an option...</option>
                </select>
                <input type="text" id="model" name="model"/>
                <button id="save">Save</button>
            </form>
        </>
    )
}