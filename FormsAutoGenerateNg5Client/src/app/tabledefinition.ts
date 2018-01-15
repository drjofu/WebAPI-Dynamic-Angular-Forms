import { PropertyDescriptorBase } from "./dynamic-forms/property-descriptor-base";

export class Tabledefinition {
  name: string;
  url: string;
  typename: string;
  primaryKey: string;
  propertyDescriptors: PropertyDescriptorBase[];
}