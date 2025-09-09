import { useState, type ChangeEvent, type FormEvent } from "react";
import type FormPropsHandler from "../models/props/form-props";
import type { ServiceResult } from "../value-objects/service-result";
import { isValidEmail, isValidPassword } from "../validates/is-valid-input";
import type { AccountModel } from "../models/account-model";

function UseForm<T extends object, TBackEndEntity extends AccountModel>(initialData: T, onSubmit: (data: T) => Promise<ServiceResult<TBackEndEntity>>)
: FormPropsHandler<T, TBackEndEntity> {
    const[formData, setFormData] = useState<T>(initialData);
    const [isSubmitting, setIsSubmitting] = useState<boolean>(false);
    const[userNameErrorMessage, setUserNameErrorMessage] = useState<string | "">("");
    const[passwordErrorMessage, setPasswordErrorMessage] = useState<string | "">("");

    const handleChange = (event: ChangeEvent<HTMLInputElement>, isCheckUserName: boolean = false, 
        isCheckPassword: boolean = false, isCheckbox: boolean = false) => {
        const {name, value, type, checked} = event.target;
        let finalValue: string | boolean = value;
        if (isCheckUserName && !isValidEmail(value))
            setUserNameErrorMessage("Email không hợp lệ.");
        else if (isCheckPassword && !isValidPassword(value)) 
            setPasswordErrorMessage("Mật khẩu không hợp lệ.");
        else if (isCheckbox || type == "checkbox")
            finalValue = checked;

        setFormData((prevData) => ({...prevData, [name]: finalValue}));
        if (userNameErrorMessage.length > 0 || passwordErrorMessage.length > 0)
            return false;
        return true;
    }

    const handleSubmit = async (event: FormEvent): Promise<ServiceResult<TBackEndEntity>> => {
        event.preventDefault();
        setIsSubmitting(true);
        setUserNameErrorMessage("");
        setPasswordErrorMessage("");
        try {
            return await onSubmit(formData);
        } catch (error) {
            if (error instanceof Error)
            {
                setUserNameErrorMessage(error.message);
                setPasswordErrorMessage(error.message);
            }
            else
                setPasswordErrorMessage('An unknown error occurred during submission.');
            // Return a failed ServiceResult<TBackEndEntity> object
            return {} as ServiceResult<TBackEndEntity>;
        } finally {
            setIsSubmitting(false);
        }
    }

    return {formData, setFormData, handleChange, isSubmitting, handleSubmit, userNameErrorMessage, passwordErrorMessage}
}

export default UseForm;