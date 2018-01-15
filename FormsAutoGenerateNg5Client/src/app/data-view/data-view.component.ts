import { Component, OnInit, Input, Output, EventEmitter } from '@angular/core';
import { Tabledefinition } from '../tabledefinition';
import { PropertyDescriptorBase } from '../dynamic-forms/property-descriptor-base';
import { ApiService } from '../api.service';
import { PropertyDescriptorTextbox } from '../dynamic-forms/property-descriptor-textbox';
import { PropertyDescriptorSelector } from '../dynamic-forms/property-descriptor-selector';
import { fail } from 'assert';

@Component({
  selector: 'data-view',
  templateUrl: './data-view.component.html',
  styleUrls: ['./data-view.component.css']
})
export class DataViewComponent implements OnInit {

  // Event, um anzuzeigen, ob der Editor aktiv ist oder nicht
  @Output() editorActiveChanged = new EventEmitter<boolean>();

  // Property-Beschreibungen für ausgewählte Tabelle
  propertyDescriptors: PropertyDescriptorBase[];

  // Die Daten dazu
  public data: object[];

  // Ausgewähltes Objekt für Editor
  public selectedEntity: object;

  // true, wenn Objekt bearbeitet wird
  public editorActive = false;

  // true, wenn neues Objekt bearbeitet wird
  public editNewEntity = false;

  // Daten stehen bereit (nach Laden von Server)
  public dataReady = false;

  // Metadaten der darzustellenden Tabelle
  private _tabledefinition: Tabledefinition;
  @Input() get tabledefinition() { return this._tabledefinition; }
  set tabledefinition(value) {
    this.propertyDescriptors = value.propertyDescriptors;
    this._tabledefinition = value;
    this.getData();
  }

  constructor(private apiService: ApiService) { }

  ngOnInit() {
  }

  // Daten laden
  async getData() {
    this.dataReady = false;

    try {
      // Primäre Daten für die ausgewählte Tabelle laden
      this.data = <object[]>await this.apiService.getTabledata(this._tabledefinition);

      // Für alle Lookup-Felder die Daten laden
      await this.apiService.resolveLookups(this.propertyDescriptors);

      this.dataReady = true;
    }
    catch (e) {
      this.showError(e);
    }

  }

  // Editor anzeigen und Form initialisieren
  edit(entity: object) {

    if (entity) {
      // ausgewähltes Objekt soll geändert werden
      this.selectedEntity = entity;
    }
    else {
      // ein neues Objekt wird angelegt
      this.editNewEntity = true;
      this.selectedEntity = {};

      // Primary Key setzen (Einschränkung: nur ein Key vom Typ int erlaubt)
      this.selectedEntity[this._tabledefinition.primaryKey] = 0
    }

    // Objekt ist jetzt in Bearbeitung
    this.editorActive = true;

    this.editorActiveChanged.emit(this.editorActive);
  }

  // Entität löschen
  async delete(entity: object) {

    // Sind Sie sicher...
    if (!confirm("Wirklich löschen?")) return;

    try {
      // und weg damit
      await this.apiService.deleteEntity(this._tabledefinition, entity[this._tabledefinition.primaryKey]);
    }
    catch (e) {
      this.showError(e);
    }

    // Daten neu laden
    await this.getData();
  }

  // Daten für Tabellendarstellung laden
  getValue(entity: object, propertyDescriptor: PropertyDescriptorBase) {

    // Der Originalwert
    let value = entity[propertyDescriptor.name];

    // Falls ein Datum dargestellt werden soll, umwandeln
    if (propertyDescriptor.displayType == "date")
      value = new Date(value).toLocaleDateString();//.toISOString();

    // dito für Zahlenwerte
    if (propertyDescriptor.displayType == "number" && value.toLocaleString)
      value = value.toLocaleString();

    // für die einfache Text-Darstellung war's das
    if (propertyDescriptor.controlType == "textbox") return value;

    // für Lookup-Felder muss der Lookup-Wert ermittelt werden
    if (propertyDescriptor.controlType == "dropdown") {
      let options = (<PropertyDescriptorSelector>propertyDescriptor).options;
      if (!options) return "";
      let o = options.find(o => o.key == value);
      if (o) return o.value;
    }

    // äh - das war nicht geplant
    return "???";
  }

  // Objekt speichern
  async saveEntity(entity: object) {
    
    // let d = JSON.stringify(entity);
    // alert("Save: " + d);

    // Editor wird geschlossen
    this.selectedEntity = undefined;

    try {

      // war das was neues?
      if (this.editNewEntity) {
        // ja, hinzufügen mit POST
        await this.apiService.postEntityData(this._tabledefinition, entity);
        this.editNewEntity = false;
      }
      else {
        // nein, Ändern mit PUT
        await this.apiService.putEntityData(this._tabledefinition, entity);
      }
    }
    catch (e) {
      this.showError(e);
    }

    // Daten neu laden
    this.data = <object[]>await this.apiService.getTabledata(this._tabledefinition);

    // Alle Buttons wieder freischalten
    this.editorActive = false;
    this.editorActiveChanged.emit(this.editorActive);

  }

  // Benutzer mag doch nicht
  cancelled() {

    // Editor schließen und Buttons freigeben
    this.selectedEntity = undefined;
    this.editorActive = false;
    this.editNewEntity = false;
    this.editorActiveChanged.emit(this.editorActive);
  }

  // zu Debug-Zwecken Fehlermeldung in Messagebox anzeigen
  showError(e: any) {
    try {
      let text = e.statusText + "\n";

      for (var t in e.error) text += "\n" + t + ": " + e.error[t][0];
      alert(text);
    }
    catch (e) { }
  }


}
