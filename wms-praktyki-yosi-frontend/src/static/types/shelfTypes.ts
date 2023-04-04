export type newShelf = {
  magazineId: number;
  position: string;
  maxload: number;
};


export type shelfDto = {
  id: number,
  position: string,
  maxQuantity: number,
  totalQuantity: number,
  freeSpace:number
}

export type detailedShelf ={
  id : number,
  position: string,
  maxQuantity: number,
  totalQuantity: number,
  freeSpace:number,
  locations: [{
    position: string;
    magazineId: number;
    quantity: number;
    productId: number;
    tag: string;
  }]
}