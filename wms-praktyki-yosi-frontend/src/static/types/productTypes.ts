import { productLocation } from './locationTypes';

export type product = {
  id: number;
  productName: string;
  ean: string;
  price: number;
  quantity: number;
  locations?: productLocation[];
};

export type productToAdd = {
  productName: string;
  ean: string;
  price: number;
};
