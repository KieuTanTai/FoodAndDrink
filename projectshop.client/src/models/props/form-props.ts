import type { ChangeEvent, FormEvent } from "react";
import type { ServiceResult } from "../../value-objects/service-result";

export default interface FormPropsHandler<T, TBackEndEntity> {
    formData: T;
    isSubmitting: boolean;
    userNameErrorMessage?: string;
    passwordErrorMessage?: string;
    setFormData: (data: T) => void;
    handleChange: (
        e: ChangeEvent<HTMLInputElement>,
        isCheckUsername?: boolean,
        isCheckPasswordValid?: boolean,
        isCheckbox?: boolean
    ) => boolean;
    handleSubmit: (e: FormEvent<Element>) => ServiceResult<TBackEndEntity> | Promise<ServiceResult<TBackEndEntity>>;
    handleCopy: (e: FormEvent<Element>) => void;
}