export class RegisterUserDTO {
    constructor(
        public email: string,
        public firstName: string,
        public lastName: string,
        public password: string,
        public rePassword: string,
        public address: string,
    ) { }
}