import { Component, OnInit } from '@angular/core';
import { ApiService } from '../api.service';
import { ActivatedRoute, Params } from '@angular/router';
import { Tabledefinition } from '../tabledefinition';

@Component({
  selector: 'main-window',
  templateUrl: './main-window.component.html',
  styleUrls: ['./main-window.component.css']
})
export class MainWindowComponent implements OnInit {

  // Beschreibungen der verfügbaren Datenbanktabellen
  public tabledefinitions: Tabledefinition[];

  // ausgewählte Tabelle
  private selectedTabledefinitionName: string;
  public selectedTabledefinition: Tabledefinition;

  // Steuerung der Navigationsbuttons
  public canNavigate: boolean = true;

  constructor(private apiService: ApiService, private route: ActivatedRoute) { }

  async ngOnInit() {

    // Informationen zu verfügbaren Tabellen laden
    this.tabledefinitions = await this.apiService.getTabledefinitions();

    // Tabellenname ist Routenparameter. Änderungen beobachten und ggf. Tabellenauswahl anpassen
    this.route.params.subscribe(async (params: Params) => {
      this.selectedTabledefinitionName = params["tablename"];
      if (this.tabledefinitions) {
        this.selectedTabledefinition = this.tabledefinitions.find(t => t.name == this.selectedTabledefinitionName);
      }
    })
  }

  // Data-View meldet, ob Navigation erlaubt ist oder nicht
  editorActiveChanged(status: boolean) {
    this.canNavigate = !status;
  }
}
