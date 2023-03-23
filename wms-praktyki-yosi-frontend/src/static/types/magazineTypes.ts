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
};

export type magazineToEdit = {
  name: string;
  address: string;
};

export type shelf = {
  position: string;
}
