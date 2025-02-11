export const validateUserLoginForm = (data, error) => {
    if (!data.login) {
        error.login = 'Логин не может быть пустым'
    }

    if (!data.password) {
        error.password = 'Пароль не может быть пустым'
    }

    return  Object.keys(error).length === 0
}

export const validateUserRegisterForm = (data, error) => {
    if (!data.login) {
        error.login = 'Логин не может быть пустым'
    }

    if (!data.password) {
        error.password = 'Пароль не может быть пустым'
    } else if (!/.{7,}/.test(data.password)) {
        error.password = 'Пароль должен содержать хотя бы 7 символов'
    } else if (!/(?=.*[0-9])/.test(data.password)) {
        error.password = 'Пароль должен содержать хотя бы 1 цифру'
    } else if (!/(?=.*[a-z])/.test(data.password)) {
        error.password = 'Пароль должен содержать хотя бы 1 строчную букву'
    } else if (!/(?=.*[A-Z])/.test(data.password)) {
        error.password = 'Пароль должен содержать хотя бы 1 заглавную букву'
    }

    if (!data.passwordConfirm || data.password !== data.passwordConfirm) {
        error.passwordConfirm = 'Пароли не совпадают'
    }
    return  Object.keys(error).length === 0
}