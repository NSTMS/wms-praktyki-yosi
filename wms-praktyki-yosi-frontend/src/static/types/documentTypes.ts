export type document = {
    id : string, 
    date : string,
    magazineId : number,
    client : string, 
    totalQuantity : number,
    quantityDone : number, 
    finished : boolean,
}
export type detailedDocument = {
    id : string, 
    date : string,
    magazineId : number,
    client : string, 
    totalQuantity : number,
    quantityDone : number, 
    finished : boolean,
    items : documentItem[]
}

export type documentItem = {
    id: string, 
    productName : string,
    postion : string,
    arriving : boolean,
    quantityPlaned :number,
    quantityDone : number, 
    tag : string,
    status : string 
}