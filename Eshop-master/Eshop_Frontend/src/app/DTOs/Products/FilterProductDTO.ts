import { Product } from "./Product";
import { ProductOrderBy } from "./ProductOrderBy";

export class FilterProductDTO {
    constructor(
        public title: string = '',
        public startPrice: number,
        public endPrice: number,
        public pageId: number,
        public pageCount: number,
        public startPage: number,
        public endPage: number,
        public takeEntity: number,
        public skipEntity: number,
        public activePage: number,
        public orderBy: ProductOrderBy = null,
        public categories: number[],
        public products: Product[]
    ) {

    }
}