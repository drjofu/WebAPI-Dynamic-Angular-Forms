import { Injectable } from '@angular/core';
import { Http } from '@angular/http';
import { HttpClient } from '@angular/common/http';
import { PropertyDescriptorBase } from './dynamic-forms/property-descriptor-base';
import { PropertyDescriptorSelector } from './dynamic-forms/property-descriptor-selector';
import { Tabledefinition } from './tabledefinition';

@Injectable()
export class ApiService {

  private baseUrl = "http://localhost:33554/api/";

  constructor(private httpClient: HttpClient) {
  }

  // Laden aller Tabellen-Metadaten
  public getTabledefinitions() {
    return <Promise<Tabledefinition[]>>this.httpClient.get(`${this.baseUrl}metadata/tabledefinitions`).toPromise();
  }

  // Laden der Metadaten für einen bestimmten Typ
  // public getPropertyDescriptions(typename: string) {
  //   return <Promise<PropertyDescriptorBase[]>>this.httpClient.get(`${this.baseUrl}metadata/${typename}`).toPromise();
  // }

  // Tabellendaten laden
  public getTabledata(tabledefinition: Tabledefinition) {
    return this.httpClient.get(`${this.baseUrl}${tabledefinition.url}`).toPromise();
  }

  // Änderungen speichern
  public putEntityData(tabledefinition: Tabledefinition, data: object) {
    return this.httpClient.put(`${this.baseUrl}${tabledefinition.url}/${data[tabledefinition.primaryKey]}`, data).toPromise();
  }

  // Neue Entität hinzufügen
  public postEntityData(tabledefinition: Tabledefinition, data: object) {
    return this.httpClient.post(`${this.baseUrl}${tabledefinition.url}`, data).toPromise();
  }

  // Datensatz löschen
  public deleteEntity(tabledefinition:Tabledefinition, id:any){
    return this.httpClient.delete(`${this.baseUrl}${tabledefinition.url}/${id}`).toPromise();
  }

  // Daten für DropDown-Listen laden
  public async resolveLookups(propertyDescriptors: PropertyDescriptorBase[]) {

    for (let pd of propertyDescriptors) {
      if (pd.controlType == 'dropdown') {
        // Metadaten für Dropdown
        let spd = <PropertyDescriptorSelector>pd;

        // Lookup-Daten laden
        let values = <object[]>await this.httpClient.get(`${this.baseUrl}${spd.lookupUrl}`).toPromise();

        // options-Array setzen mit key- und value-Feld
        spd.options = values.map(v => { return { key: v[spd.lookupIdField], value: v[spd.lookupValueField] } });
      }
    }
  }
}
