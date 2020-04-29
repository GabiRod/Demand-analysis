<template>
  <div id="client_table_component" class="client_table_component">
    <thead>
      <tr>
        <th
          class="clients_column_menu"
          v-for="col in columns"
          v-on:click="sortTable(col)"
          :key="col"
        >
          {{ col }}
          <div
            class="arrow"
            v-if="col == sortColumn"
            v-bind:class="[ascending ? 'arrow_up' : 'arrow_down']"
          ></div>
        </th>
      </tr>
    </thead>
    <tbody>
      <tr class="clients_row" v-for="row in get_rows()" :key="row">
        <td class="clients_row_data" v-for="col in columns" :key="col">
          {{ row[col] }}
        </td>
      </tr>
    </tbody>
  </div>
</template>

<script>
export default {
  name: "clientTableComponent",
  data() {
    return {
      currentPage: 1,
      elementsPerPage: 100,
      ascending: false,
      sortColumn: "",
      rows: [
        {
          id: 1,
          name: "Chandler Bing",
          phone: "305-917-1301",
          profession: "IT Manager",
        },
        {
          id: 2,
          name: "Ross Geller",
          phone: "210-684-8953",
          profession: "Paleontologist",
        },
        {
          id: 3,
          name: "Rachel Green",
          phone: "765-338-0312",
          profession: "Waitress",
        },
        {
          id: 4,
          name: "Monica Geller",
          phone: "714-541-3336",
          profession: "Head Chef",
        },
        {
          id: 5,
          name: "Joey Tribbiani",
          phone: "972-297-6037",
          profession: "Actor",
        },
        {
          id: 6,
          name: "Phoebe Buffay",
          phone: "760-318-8376",
          profession: "Masseuse",
        },
      ],
    };
  },

  methods: {
    sortTable: function sortTable(col) {
      if (this.sortColumn === col) {
        this.ascending = !this.ascending;
      } else {
        this.ascending = true;
        this.sortColumn = col;
      }

      var ascending = this.ascending;

      this.rows.sort(function(a, b) {
        if (a[col] > b[col]) {
          return ascending ? 1 : -1;
        } else if (a[col] < b[col]) {
          return ascending ? -1 : 1;
        }
        return 0;
      });
    },
    get_rows: function get_rows() {
      var start = (this.currentPage - 1) * this.elementsPerPage;
      var end = start + this.elementsPerPage;
      return this.rows.slice(start, end);
    },
  },

  computed: {
    columns: function columns() {
      if (this.rows.length == 0) {
        return [];
      }
      return Object.keys(this.rows[0]);
    },
  },
};
</script>

<style lang="scss">
.client_table_component {
  background-color: $white;
}

.clients_column_menu {
  padding: 5px 10px;
  border-bottom: 1px solid $grey;
  color: grey;
}

.clients_row {
  width: 100%;
}

.clients_row_data {
  color: $blue;
  padding: 5px 10px;
  border-bottom: 1px solid $grey;
}
</style>
