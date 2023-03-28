import { productLocation } from '@static/types/locationTypes';

export type magazine = {
  id: number;
  name: string;
  address: string;
  shelves?: shelf[];
};

export type magazineToAdd = {
  name: string;
  address: string;
  dimentions: string;
  shelvesPerRow: number;
  maxShelfQuantity:number;
};


export type shelf = {
  position: string;
}

export type magazineDto  ={
  name: string;
  address: string;
  dimentions: string;
  shelvesPerRow: number;
  maxShelfCapacity:number;
  shelfNumber : number;
  totalCapacity : number;
  totalQuantity : number;
}

export type magazineToEdit = {
  name: string;
  address: string;
}