export class Mapcell {
    ship: boolean;
    damaged: boolean;
    hidden: boolean;
    hover: boolean;
    constructor(ship: boolean, damaged: boolean, hidden: boolean) {
        this.ship = ship;
        this.damaged = damaged;
        this.hidden = hidden;
    }
}

