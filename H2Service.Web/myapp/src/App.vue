<template>
  <v-app>
    <v-toolbar dark color="primary">
      <v-toolbar-title class="headline text-uppercase">
        <span>H2Service</span>       
      </v-toolbar-title>
      <v-spacer></v-spacer>     
        <v-avatar
          :tile="tile"
          :size="avatarSize"
          color="grey lighten-4"
        >
          <img src="http://shp.qpic.cn/bizmp/nD7oZq0vvo97KV4coACXPz74zGorqhn5dJopQf83EuDIicVjfUttWjQ/" alt="avatar">
        </v-avatar>
     
    </v-toolbar>

   <div class="text-xs-center">
      <v-content>
       <v-dialog v-model="dialog" persistent max-width="600px">
      <v-btn slot="activator" color="primary" dark>Open Dialog</v-btn>
      <v-card>
        <v-card-title>
          <span class="headline">User Profile</span>
        </v-card-title>
        <v-card-text>
          <v-container grid-list-md>
            <v-layout wrap>
              <v-flex xs12 sm6 md4>
                <v-text-field label="Legal first name*" required></v-text-field>
              </v-flex>
              <v-flex xs12 sm6 md4>
                <v-text-field label="Legal middle name" hint="example of helper text only on focus"></v-text-field>
              </v-flex>
              <v-flex xs12 sm6 md4>
                <v-text-field
                  label="Legal last name*"
                  hint="example of persistent helper text"
                  persistent-hint
                  required
                ></v-text-field>
              </v-flex>
              <v-flex xs12>
                <v-text-field label="Email*" required></v-text-field>
              </v-flex>
              <v-flex xs12>
                <v-text-field label="Password*" type="password" required></v-text-field>
              </v-flex>
              <v-flex xs12 sm6>
                <v-select
                  :items="['0-17', '18-29', '30-54', '54+']"
                  label="Age*"
                  required
                ></v-select>
              </v-flex>
              <v-flex xs12 sm6>
                <v-autocomplete
                  :items="['Skiing', 'Ice hockey', 'Soccer', 'Basketball', 'Hockey', 'Reading', 'Writing', 'Coding', 'Basejump']"
                  label="Interests"
                  multiple
                ></v-autocomplete>
              </v-flex>
            </v-layout>
          </v-container>
          <small>*indicates required field</small>
        </v-card-text>
        <v-card-actions>
          <v-spacer></v-spacer>
          <v-btn color="blue darken-1" flat @click="dialog = false">Close</v-btn>
          <v-btn color="blue darken-1" flat @click="dialog = false">Save</v-btn>
        </v-card-actions>
      </v-card>
    </v-dialog>
    </v-content>
   </div>
  </v-app>
</template>

<script>

export default {
  name: 'App',
  data () {
    return {
      dialog: true  //
    }
  },
  methods:{
    go:function(){
      alert(1)
    }
  }
}
</script>
