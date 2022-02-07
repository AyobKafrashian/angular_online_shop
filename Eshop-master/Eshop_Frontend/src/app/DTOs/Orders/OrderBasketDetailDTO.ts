export class OrderBasketDetailDTO {
    constructor(
        public id:number,
        public title: string,
        public price: number,
        public imageNama: string,
        public count: number,
    ) { }
}