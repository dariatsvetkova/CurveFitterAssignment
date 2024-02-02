import React, { useState } from "react";

interface SavePlotFormProps {
    handleSavePlot: (
        e: React.FormEvent<HTMLFormElement>,
        input: string,
    ) => void;
    hideForm: () => void;
    submitting: boolean;
}

export default function SavePlotForm({
    handleSavePlot,
    hideForm,
    submitting,
}: SavePlotFormProps) {
    const [input, setInput] = useState('')

    return (
        <form
            onSubmit={(e) => handleSavePlot(e, input)}
        >
            <div className="inputContainer">
                <label htmlFor="name" className="formLabel">
                    Give your plot a distinct name:
                </label>
                <input
                    type="text"
                    id="name"
                    name="name"
                    required
                    value={input}
                    onChange={(e) => setInput(e.target.value)}
                />
            </div>

            <div className="formButtonContainer">
                <button
                    type="button"
                    onClick={() => hideForm()}
                    disabled={submitting}
                >
                    Cancel
                </button>

                <button
                    type="submit"
                    disabled={submitting}
                >
                    Save
                </button>
            </div>
        </form>
    )
}