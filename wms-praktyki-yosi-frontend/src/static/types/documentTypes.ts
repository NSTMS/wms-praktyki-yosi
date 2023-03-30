export type document = {
  id: string;
  date: string;
  magazineId: number;
  client: string;
  totalQuantity: number;
  quantityDone: number;
  finished: boolean;
};
export type detailedDocument = {
  id: string;
  date: string;
  magazineId: number;
  client: string;
  totalQuantity: number;
  quantityDone: number;
  finished: boolean;
  items: documentItem[];
};

export type documentItem = {
  id: string;
  productName: string;
  productId : number;
  postion: string;
  arriving: boolean;
  quantityPlaned: number;
  quantityDone: number;
  tag: string;
  status: string;
};


export type EditDialogData = {
  arriving : boolean;
  tag : string;
  quantity : number;
}

export type newDocument = {
  date  : string;
  client : string;
  magazineId : number;
  itemList : listItem[];
}

export type listItem = {
  productName : string;
  arriving : boolean;
  quantity : number;
  tag : string;
  id: number;
}
export type ItemToSend ={
  productName : string;
  arriving : boolean;
  quantity : number;
  tag : string;
}

export type visitedElement = {
  productName : string;
  position : string;
  quantity : number;
  tag : string;
}