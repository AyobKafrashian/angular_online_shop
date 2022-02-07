export class ProductCategory {
    constructor(
        public id: number,
        public createDate: Date,
        public lastUpdateDate: Date,
        public isDelete: boolean,
        public parentId: number,
        public title: string,
        public urlTitle: string,
    ) { }
}